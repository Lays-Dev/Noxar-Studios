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

    [Header("Random Minigame Count")]
    [Min(1)]
    public int minMinigames = 1;

    [Min(1)]
    public int maxMinigames = 2;

    [Header("Phone Patron Special Minigame")]
    public MinigameData phoneMinigame;

    // Must match the patronName in your Phone PatronData.
    public string phonePatronName = "Phone";

    [Header("Phone Delivery Minigame")]
    public MinigameData deliveryMinigame;

    public bool deliveryMinigameUnlocked = false;

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

    [Header("Level Timer")]
    public float secondsPerMinigame = 30f;
    public float timeRemaining = 0f;
    public string loseSceneName = "Lose";

    private bool timerRunning = false;
    private bool loseSceneLoading = false;

  

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

    

    private void Update()
    {
        // Do nothing if the timer is not running.
        if (!timerRunning)
            return;

        // Count down using real time.
        timeRemaining -= Time.deltaTime;

        // Stop at zero.
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;

            // Load the Lose scene only once.
            if (!loseSceneLoading)
            {
                loseSceneLoading = true;
                SceneManager.LoadScene(loseSceneName);
            }
        }
    }

    public void StartLevelTimer()
    {
        // Give the player 30 seconds for every required minigame.
        timeRemaining =
            requiredMinigames.Count * secondsPerMinigame;

        loseSceneLoading = false;
        timerRunning = true;

        Debug.Log("Timer started: " + timeRemaining + " seconds");
    }

    public void PauseLevelTimer()
    {
        timerRunning = false;

        Debug.Log("Dish served! Level timer paused at " + timeRemaining + " seconds.");
    }

    
public void GenerateLevel()
{
    if (availablePatrons == null || availablePatrons.Length == 0 ||
        availableDishes == null || availableDishes.Length == 0 ||
        availableMinigames == null || availableMinigames.Length == 0)
    {
        Debug.LogError(
            "LevelManager: Add patrons, dishes, and minigames in the Inspector."
        );
        return;
    }

    // Choose a random patron and dish.
    currentPatron = availablePatrons[
        Random.Range(0, availablePatrons.Length)
    ];

    currentDish = availableDishes[
        Random.Range(0, availableDishes.Length)
    ];

    // Reset progress for the new level.
    requiredMinigames.Clear();
    completedMinigames.Clear();

    orderTaken = false;
    levelFinished = false;
    dishServed = false;
    deliveryMinigameUnlocked = false;

    // Check whether this is the Phone patron.
    bool isPhonePatron =
        currentPatron.patronName == phonePatronName;

    // Add the special minigame first for the Phone patron.
    if (isPhonePatron)
    {
        if (phoneMinigame == null)
        {
            Debug.LogError(
                "Phone patron was selected, but Phone Minigame is not assigned!"
            );
            return;
        }

        requiredMinigames.Add(phoneMinigame);
    }

    // Build a list of regular minigames.
    // Never include the special Phone minigame in random selection.
    List<MinigameData> available = new List<MinigameData>();

    foreach (MinigameData minigame in availableMinigames)
    {
        if (minigame == null)
            continue;

        if (!minigame.availableForRandomSelection)
            continue;

        if (minigame == phoneMinigame)
            continue;

        available.Add(minigame);
    }

    // Choose a random number of regular minigames.
    // These are additional to the required Phone minigame.
    if (available.Count > 0)
    {
        int minimum = Mathf.Clamp(minMinigames, 1, available.Count);
        int maximum = Mathf.Clamp(maxMinigames, minimum, available.Count);

        int amount = Random.Range(minimum, maximum + 1);

        // Choose regular minigames without duplicates.
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, available.Count);

            requiredMinigames.Add(available[randomIndex]);
            available.RemoveAt(randomIndex);
        }
    }
    else if (!isPhonePatron)
    {
        Debug.LogError(
            "No regular minigames are available for this patron!"
        );
        return;
    }
    // Add Delivery LAST for the Phone patron.
    if (isPhonePatron)
    {
        if (deliveryMinigame == null)
        {
            Debug.LogError("Delivery minigame is not assigned!");
            return;
        }

        // Make sure Delivery wasn't selected as a regular minigame.
        requiredMinigames.Remove(deliveryMinigame);

        // Add Delivery as the final required minigame.
        requiredMinigames.Add(deliveryMinigame);
    }

    Debug.Log(
        "Level " + currentLevel +
        " generated! Patron: " + currentPatron.patronName +
        ", Dish: " + currentDish.dishName +
        ", Minigames: " + requiredMinigames.Count
    );

    foreach (MinigameData minigame in requiredMinigames)
    {
        Debug.Log("Required minigame: " + minigame.minigameName);
    }

    // Start the timer for the new level.
    StartLevelTimer();
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

    // Check whether Delivery is the final required minigame.
    if (currentPatron != null &&
        currentPatron.patronName == phonePatronName &&
        deliveryMinigame != null &&
        requiredMinigames.Contains(deliveryMinigame))
    {
        bool allOtherMinigamesComplete = true;

        foreach (MinigameData required in requiredMinigames)
        {
            if (required == deliveryMinigame)
                continue;

            if (!completedMinigames.Contains(required))
            {
                allOtherMinigamesComplete = false;
                break;
            }
        }

        deliveryMinigameUnlocked = allOtherMinigamesComplete;
    }

    Debug.Log(
        "Completed " + minigame.minigameName +
        " (" + completedMinigames.Count +
        "/" + requiredMinigames.Count + ")"
    );

    if (deliveryMinigameUnlocked)
    {
        Debug.Log("All other Phone order minigames are complete! Delivery is unlocked.");
    }
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
