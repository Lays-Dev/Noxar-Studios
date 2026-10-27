using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Available Content")]
    public PatronData[] availablePatrons;
    public DishData[] availableDishes;
    public MinigameData[] availableMinigames;

    [Header("Level Settings")]
    [Min(1)]
    public int minigamesPerLevel = 2;

    [Header("Current Level")]
    public int currentLevel = 1;
    public PatronData currentPatron;
    public DishData currentDish;

    public List<MinigameData> requiredMinigames =
        new List<MinigameData>();

    public List<MinigameData> completedMinigames =
        new List<MinigameData>();

    [Header("Order Progress")]
    public bool orderTaken = false;
    public bool levelFinished = false;
    public bool dishServed = false;

    private void Awake()
    {
        // Keep only one LevelManager between scenes.
        // Singleton Design Pattern: Ensures only one instance of LevelManager exists.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Generate the first level only once.
        if (currentPatron == null ||
            currentDish == null ||
            requiredMinigames.Count == 0)
        {
            GenerateLevel();
        }
    }

    public void GenerateLevel()
    {
        if (availablePatrons == null ||
            availablePatrons.Length == 0 ||
            availableDishes == null ||
            availableDishes.Length == 0 ||
            availableMinigames == null ||
            availableMinigames.Length == 0)
        {
            Debug.LogError(
                "LevelManager: Add patrons, dishes, and minigames in the Inspector."
            );
            return;
        }

        // Choose a random patron and dish independently.
        currentPatron = availablePatrons[
            Random.Range(0, availablePatrons.Length)
        ];

        currentDish = availableDishes[
            Random.Range(0, availableDishes.Length)
        ];

        // Start this level with fresh progress.
        requiredMinigames.Clear();
        completedMinigames.Clear();

        orderTaken = false;
        levelFinished = false;
        dishServed = false;

        // Collect minigames enabled for random selection.
        List<MinigameData> available = new List<MinigameData>();

        foreach (MinigameData minigame in availableMinigames)
        {
            if (minigame != null &&
                minigame.availableForRandomSelection)
            {
                available.Add(minigame);
            }
        }

        if (available.Count == 0)
        {
            Debug.LogError(
                "LevelManager: No minigames are enabled for random selection."
            );
            return;
        }

        // Don't request more unique minigames than are available.
        int amount = Mathf.Min(
            Mathf.Max(1, minigamesPerLevel),
            available.Count
        );

        // Randomly select minigames without duplicates.
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, available.Count);

            requiredMinigames.Add(available[randomIndex]);
            available.RemoveAt(randomIndex);
        }

        Debug.Log(
            "Level " + currentLevel +
            " generated! Patron: " + currentPatron.patronName +
            ", Dish: " + currentDish.dishName +
            ", Minigames: " + requiredMinigames.Count
        );
    }

    public void TakeOrder()
    {
        if (currentPatron == null || currentDish == null)
            return;

        if (orderTaken)
            return;

        orderTaken = true;

        Debug.Log(
            currentPatron.patronName +
            " ordered " + currentDish.dishName
        );
    }

    public void MarkMinigameComplete(MinigameData minigame)
    {
        if (minigame == null)
            return;

        if (!requiredMinigames.Contains(minigame))
            return;

        // Don't count the same minigame twice.
        if (!completedMinigames.Contains(minigame))
        {
            completedMinigames.Add(minigame);
        }

        Debug.Log(
            "Completed " + minigame.minigameName +
            " (" + completedMinigames.Count +
            "/" + requiredMinigames.Count + ")"
        );
    }

    public bool AreAllMinigamesComplete()
    {
        return requiredMinigames.Count > 0 &&
               completedMinigames.Count == requiredMinigames.Count;
    }

    public void ReturnToCounter()
    {
        SceneManager.LoadScene("OrderCounter");
    }

    public void FinishLevel()
    {
        if (!AreAllMinigamesComplete())
        {
            Debug.Log("Complete all required minigames first!");
            return;
        }

        levelFinished = true;

        Debug.Log("Level requirements completed! Serve the dish.");
    }

    public void StartNextLevel()
    {
        if (!levelFinished)
        {
            Debug.Log("Finish the current level before starting another.");
            return;
        }

        currentLevel++;

        GenerateLevel();

        SceneManager.LoadScene("Counter");
    }
}
