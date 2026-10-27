using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KitchenManager : MonoBehaviour
{
    [Header("Minigame Display")]
    public Transform minigameContainer;

    [Header("Progress UI")]
    public TMP_Text progressText;
    public Button returnToCounterButton;

    private LevelManager levelManager;

    private MinigameData currentMinigame;
    private GameObject currentMinigameObject;

    private bool allComplete = false;

    private void Start()
    {
        levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError(
                "KitchenManager: LevelManager was not found!"
            );
            return;
        }

        if (!levelManager.orderTaken)
        {
            Debug.LogWarning(
                "KitchenManager: Take the patron's order first."
            );
            return;
        }

        if (minigameContainer == null)
        {
            Debug.LogError(
                "KitchenManager: Assign the Minigame Container."
            );
            return;
        }

        if (returnToCounterButton != null)
        {
            returnToCounterButton.onClick.RemoveListener(
                ReturnToCounter
            );

            returnToCounterButton.onClick.AddListener(
                ReturnToCounter
            );

            // The player cannot leave until all minigames are done.
            returnToCounterButton.interactable = false;
        }

        ShowNextMinigame();
    }

    public void CompleteCurrentMinigame()
    {
        if (levelManager == null || currentMinigame == null)
            return;

        // Record this minigame as complete.
        levelManager.MarkMinigameComplete(currentMinigame);

        // Remove the current minigame UI.
        if (currentMinigameObject != null)
        {
            Destroy(currentMinigameObject);
            currentMinigameObject = null;
        }

        currentMinigame = null;

        // Display the next incomplete minigame.
        ShowNextMinigame();
    }

    private void ShowNextMinigame()
    {
        currentMinigame = null;

        // Find the next required minigame not yet completed.
        foreach (MinigameData minigame in
                 levelManager.requiredMinigames)
        {
            if (minigame == null)
                continue;

            if (levelManager.completedMinigames.Contains(minigame))
                continue;

            currentMinigame = minigame;
            break;
        }

        // No incomplete minigames remain.
        if (currentMinigame == null)
        {
            FinishMinigames();
            return;
        }

        UpdateProgressText();

        // Check that the selected minigame has a prefab.
        if (currentMinigame.minigamePrefab == null)
        {
            Debug.LogError(
                "No prefab assigned for minigame: " +
                currentMinigame.minigameName
            );
            return;
        }

        // Create the selected minigame UI inside the container.
        currentMinigameObject = Instantiate(
            currentMinigame.minigamePrefab,
            minigameContainer,
            false
        );

        // Connect its Complete button to this manager.
        MinigamePanel panel =
            currentMinigameObject.GetComponent<MinigamePanel>();

        if (panel != null)
        {
            panel.Setup(this);
        }
        else
        {
            Debug.LogError(
                "The prefab needs a MinigamePanel component: " +
                currentMinigame.minigameName
            );
        }
    }

    private void UpdateProgressText()
    {
        if (progressText == null)
            return;

        progressText.text =
            "Minigames: " +
            levelManager.completedMinigames.Count +
            " / " +
            levelManager.requiredMinigames.Count;
    }

    private void FinishMinigames()
    {
        allComplete = levelManager.AreAllMinigamesComplete();

        UpdateProgressText();

        if (allComplete)
        {
            levelManager.FinishLevel();

            if (progressText != null)
                progressText.text = "All minigames complete!";

            if (returnToCounterButton != null)
                returnToCounterButton.interactable = true;

            Debug.Log(
                "All required minigames are complete!"
            );
        }
    }

    public void ReturnToCounter()
    {
        if (!allComplete)
        {
            Debug.Log(
                "Complete all minigames before returning."
            );
            return;
        }

        levelManager.ReturnToCounter();
    }

    private void OnDestroy()
    {
        if (returnToCounterButton != null)
        {
            returnToCounterButton.onClick.RemoveListener(
                ReturnToCounter
            );
        }
    }
}
