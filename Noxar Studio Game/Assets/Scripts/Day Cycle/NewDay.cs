
/*using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDay : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject dish;
    public GameObject endDayButton;
    public GameObject currency;

    public GameObject galico;
    public GameObject johnny;

    public bool comingFromWinScreen = false;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (LevelManager.level2Unlocked == true)
        {
            johnny.SetActive(true);
            galico.SetActive(false);
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        UpdatePatron();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Order Counter Blockout")
        {
            // Find the patrons belonging to the newly loaded scene
            FindPatrons();
            UpdatePatron();
        }
    }


public void StartNewDay()
{
    // Unlock Level 2 if the player clicked New Day after winning.
    if (comingFromWinScreen && LevelManager.Instance != null)
    {
        LevelManager.Instance.UnlockLevel2();
    }

    // Reset the timer.
    if (GameTimer.Instance != null)
    {
        GameTimer.Instance.ResetTimer();
    }

    // Reset the day's progress.
    TapSprite.patronHasOrdered = false;
    TapSprite.level2OrderTaken = false;
    EndMinigame.gatherComplete = false;
    GrillingUIManager.grillingCompleted = false;

    // Hide the UI.
    if (loseScreen != null)
        loseScreen.SetActive(false);

    if (winScreen != null)
        winScreen.SetActive(false);

    if (endDayButton != null)
        endDayButton.SetActive(false);

    if (dish != null)
        dish.SetActive(false);

    if (currency != null)
        currency.SetActive(false);

    // Choose which scene to load.
    if (LevelManager.level2Unlocked)
    {
        SceneManager.LoadScene("Level 2 Instructions");
    }
    else
    {
        SceneManager.LoadScene("Order Counter Blockout");
    }
}


    private void FindPatrons()
    {
        // Search all loaded objects, including inactive patrons
        GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

        foreach (GameObject obj in objects)
        {
            if (obj.name == "Galico" &&
                obj.scene == gameObject.scene)
            {
                galico = obj;
            }

            if (obj.name == "Johnny" &&
                obj.scene == gameObject.scene)
            {
                johnny = obj;
            }
        }
    }

    private void UpdatePatron()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager is missing!");
            return;
        }

        bool level2 = LevelManager.level2Unlocked;

        if (galico != null)
            galico.SetActive(!level2);

        if (johnny != null)
            johnny.SetActive(level2);

        Debug.Log("Level 2 unlocked: " + level2);
        Debug.Log("Galico active: " +
            (galico != null && galico.activeSelf));
        Debug.Log("Johnny active: " +
            (johnny != null && johnny.activeSelf));
    }
}*/