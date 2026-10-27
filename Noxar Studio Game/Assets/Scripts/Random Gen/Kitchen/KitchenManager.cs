
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KitchenManager : MonoBehaviour
{
    [Header("Minigame UI")]
    public Transform minigameContainer;
    public GameObject minigamePanel;
    public Button interactButton;

    [Header("Navigation")]
    public Button returnToCounterButton;

    [Header("Optional UI")]
    public TMP_Text progressText;

    [Header("Player")]
    public Behaviour playerMovementScript;

    private LevelManager levelManager;
    private KitchenMinigameStation nearbyStation;

    private MinigameData currentMinigame;
    private GameObject currentMinigameObject;

    private bool minigameOpen = false;

    private void Start()
    {
        levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError("LevelManager was not found!");
            return;
        }

        // Refresh all station visibility when entering the Kitchen.
        KitchenMinigameStation[] stations =
            FindObjectsByType<KitchenMinigameStation>(
                FindObjectsSortMode.None
            );

        foreach (KitchenMinigameStation station in stations)
        {
            station.UpdateStationVisibility();
        }

        if (!levelManager.orderTaken)
        {
            Debug.LogWarning(
                "Take the patron's order before entering the kitchen."
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

        if (minigamePanel != null)
            minigamePanel.SetActive(false);


        if (returnToCounterButton != null)
        {
            returnToCounterButton.onClick.RemoveListener(ReturnToCounter);
            returnToCounterButton.onClick.AddListener(ReturnToCounter);
            returnToCounterButton.interactable = true;
        }

        UpdateProgress();
    }


    public void PlayerEnteredStation(KitchenMinigameStation station)
    {
        if (minigameOpen || levelManager == null || station == null)
            return;

        // Check whether this station is required for the current order.
        if (!IsStationRequired(station))
        {
            Debug.Log("This station is not required for the current order.");
            return;
        }

        // Do not open a minigame that is already completed.
        if (IsStationCompleted(station))
        {
            Debug.Log("This minigame is already completed.");
            return;
        }

        nearbyStation = station;

        // Open the minigame automatically.
        OpenMinigame(station);
    }

    public void PlayerLeftStation(KitchenMinigameStation station)
    {
        if (nearbyStation != station)
            return;

        nearbyStation = null;
        UpdateInteractButton();
    }

    private void UpdateInteractButton()
    {
        if (interactButton == null)
            return;

        bool canInteract =
            !minigameOpen &&
            nearbyStation != null &&
            IsStationRequired(nearbyStation) &&
            !IsStationCompleted(nearbyStation);

        interactButton.gameObject.SetActive(canInteract);
    }

    private bool IsStationRequired(KitchenMinigameStation station)
    {
        return station != null &&
               station.minigame != null &&
               levelManager.requiredMinigames.Contains(station.minigame);
    }

    private bool IsStationCompleted(KitchenMinigameStation station)
    {
        return station != null &&
               station.minigame != null &&
               levelManager.completedMinigames.Contains(station.minigame);
    }

    private void InteractWithStation()
    {
        if (minigameOpen || nearbyStation == null)
            return;

        if (!IsStationRequired(nearbyStation))
        {
            Debug.Log("This station is not required for this order.");
            UpdateInteractButton();
            return;
        }

        if (IsStationCompleted(nearbyStation))
        {
            Debug.Log("This minigame is already complete.");
            UpdateInteractButton();
            return;
        }

        currentMinigame = nearbyStation.minigame;

        if (currentMinigame.minigamePrefab == null)
        {
            Debug.LogError(
                "No prefab assigned for " + currentMinigame.minigameName
            );
            return;
        }

        currentMinigameObject = Instantiate(
            currentMinigame.minigamePrefab,
            minigameContainer,
            false
        );

        MinigamePanel panel =
            currentMinigameObject.GetComponent<MinigamePanel>();

        if (panel == null)
        {
            Debug.LogError(
                "The minigame prefab needs a MinigamePanel component."
            );

            Destroy(currentMinigameObject);
            currentMinigameObject = null;
            currentMinigame = null;
            return;
        }

        panel.Setup(this);

        minigameOpen = true;

        if (minigamePanel != null)
            minigamePanel.SetActive(true);

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        if (interactButton != null)
            interactButton.gameObject.SetActive(false);

        if (returnToCounterButton != null)
            returnToCounterButton.interactable = false;
    }

    public void CompleteCurrentMinigame()
    {
        if (!minigameOpen || currentMinigame == null)
            return;

        levelManager.MarkMinigameComplete(currentMinigame);

        // Hide the station whose minigame was just completed.
        if (nearbyStation != null)
        {
            nearbyStation.UpdateStationVisibility();
        }

        if (currentMinigameObject != null)
            Destroy(currentMinigameObject);

        currentMinigameObject = null;
        currentMinigame = null;
        minigameOpen = false;

        if (minigamePanel != null)
            minigamePanel.SetActive(false);

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        if (returnToCounterButton != null)
            returnToCounterButton.interactable = true;

        UpdateProgress();
        UpdateInteractButton();

        if (levelManager.AreAllMinigamesComplete())
        {
            levelManager.FinishLevel();
            Debug.Log("All required minigames are complete!");
        }
    }

    private void UpdateProgress()
    {
        if (progressText == null || levelManager == null)
            return;

        progressText.text =
            "Minigames: " +
            levelManager.completedMinigames.Count +
            " / " +
            levelManager.requiredMinigames.Count;
    }

    public void ReturnToCounter()
    {
        // Do not leave while actively playing a minigame.
        if (minigameOpen)
            return;

        SceneManager.LoadScene("Counter");
    }

    private void OnDestroy()
    {
        if (interactButton != null)
            interactButton.onClick.RemoveListener(InteractWithStation);

        if (returnToCounterButton != null)
            returnToCounterButton.onClick.RemoveListener(ReturnToCounter);
    }

    private void OpenMinigame(KitchenMinigameStation station)
{
    currentMinigame = station.minigame;

    if (currentMinigame == null ||
        currentMinigame.minigamePrefab == null)
    {
        Debug.LogError(
            "The station's MinigameData or Minigame Prefab is missing!"
        );
        return;
    }

    // Create the minigame UI inside the Minigame Container.
    currentMinigameObject = Instantiate(
        currentMinigame.minigamePrefab,
        minigameContainer,
        false
    );

    // Find the MinigamePanel script on the prefab.
    MinigamePanel panel =
        currentMinigameObject.GetComponent<MinigamePanel>();

    if (panel == null)
    {
        Debug.LogError(
            "The minigame prefab needs a MinigamePanel component."
        );

        Destroy(currentMinigameObject);
        currentMinigameObject = null;
        currentMinigame = null;
        return;
    }

    panel.Setup(this);

    minigameOpen = true;


    // Stop the player moving while playing the minigame.
    if (playerMovementScript != null)
        playerMovementScript.enabled = false;

    // Hide the old Interact button, if one was assigned.
    if (interactButton != null)
        interactButton.gameObject.SetActive(false);

    Debug.Log("Opened minigame: " + currentMinigame.minigameName);
}
}
