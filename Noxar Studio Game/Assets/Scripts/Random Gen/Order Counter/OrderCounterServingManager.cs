
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OrderCounterServingManager : MonoBehaviour
{
    [Header("Order UI")]
    public GameObject dialoguePanel;
    public GameObject orderTicketPanel;
    public Button serveButton;

    [Header("Served Dish")]
    public Image dishServedImage;

    [Header("End Day UI")]
    public GameObject endDayPanel;
    public TMP_Text amountEarnedText;
    public Image earningsImage;
    public Button newLevelButton;

    [Header("Earnings Settings")]
    public int earningsPerMinigame = 50;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError(
                "OrderCounterServingManager: LevelManager not found!"
            );
            return;
        }

        // Connect the Serve button.
        if (serveButton != null)
        {
            serveButton.onClick.RemoveListener(ServeDish);
            serveButton.onClick.AddListener(ServeDish);
        }

        // Connect the New Level button.
        if (newLevelButton != null)
        {
            newLevelButton.onClick.RemoveListener(StartNewLevel);
            newLevelButton.onClick.AddListener(StartNewLevel);
        }

        // Update the UI when entering the Order Counter.
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (levelManager == null)
            return;

        // Show the Serve button only when all minigames are complete.
        bool canServe =
            levelManager.orderTaken &&
            levelManager.AreAllMinigamesComplete() &&
            !levelManager.dishServed;

        if (serveButton != null)
            serveButton.gameObject.SetActive(canServe);

        // Restore the End Day screen if the dish was already served.
        if (levelManager.dishServed)
        {
            ShowEndDay();
        }
        else
        {
            if (dishServedImage != null)
                dishServedImage.gameObject.SetActive(false);

            if (endDayPanel != null)
                endDayPanel.SetActive(false);
        }
    }

    public void ServeDish()
    {
        if (levelManager == null)
            return;

        // Do not serve before all required minigames are complete.
        if (!levelManager.AreAllMinigamesComplete())
        {
            Debug.Log("Complete all required minigames first!");
            return;
        }

        if (levelManager.dishServed)
            return;

        if (levelManager.currentDish == null)
        {
            Debug.LogError("No dish has been assigned to the current order!");
            return;
        }

        // Mark the dish as served.
        levelManager.dishServed = true;

        // Make sure the level is marked as finished.
        levelManager.FinishLevel();

        // Show the finished dish.
        if (dishServedImage != null)
        {
            dishServedImage.sprite =
                levelManager.currentDish.dishSprite;

            dishServedImage.preserveAspect = true;
            dishServedImage.gameObject.SetActive(true);
        }

        // Hide the dialogue and order ticket.
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (orderTicketPanel != null)
            orderTicketPanel.SetActive(false);

        if (serveButton != null)
            serveButton.gameObject.SetActive(false);

        // Calculate earnings from completed minigames.
        int amountEarned =
            levelManager.completedMinigames.Count *
            earningsPerMinigame;

        if (amountEarnedText != null)
        {
            amountEarnedText.text =
                "+" + amountEarned + " Galaxy Cubes!";
        }

        // Display the End Day panel.
        ShowEndDay();

        Debug.Log("+" + amountEarned + " Galaxy Cubes!");
    }

    private void ShowEndDay()
    {
        if (endDayPanel != null)
            endDayPanel.SetActive(true);

        if (dishServedImage != null)
            dishServedImage.gameObject.SetActive(true);

        if (earningsImage != null)
            earningsImage.gameObject.SetActive(true);

        if (serveButton != null)
            serveButton.gameObject.SetActive(false);
    }

    public void StartNewLevel()
    {
        if (levelManager == null)
            return;

        if (!levelManager.dishServed)
        {
            Debug.Log("Serve the dish before starting a new level.");
            return;
        }

        // GenerateLevel clears completed minigames, selects a new
        // dish and patron, and chooses a new set of minigames.
        levelManager.StartNextLevel();
    }

    private void OnDestroy()
    {
        if (serveButton != null)
            serveButton.onClick.RemoveListener(ServeDish);

        if (newLevelButton != null)
            newLevelButton.onClick.RemoveListener(StartNewLevel);
    }
}
