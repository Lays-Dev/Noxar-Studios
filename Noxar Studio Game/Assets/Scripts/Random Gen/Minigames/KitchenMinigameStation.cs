
using UnityEngine;

public class KitchenMinigameStation : MonoBehaviour
{
    [Header("Station Information")]
    public MinigameData minigame;

    [Header("Station Visibility")]
    public GameObject stationObject;

    private KitchenManager kitchenManager;

    private void Start()
    {
        kitchenManager = FindFirstObjectByType<KitchenManager>();

        if (minigame == null)
        {
            Debug.LogError(
                name + ": Assign a MinigameData asset!",
                this
            );
            return;
        }

        // Check whether this minigame was selected for the order.
        UpdateStationVisibility();
    }


public void UpdateStationVisibility()
{
    if (minigame == null || LevelManager.Instance == null)
        return;

    // Is this minigame required for the current order?
    bool isRequired =
        LevelManager.Instance.requiredMinigames.Contains(minigame);

    // Has this minigame already been completed?
    bool isCompleted =
        LevelManager.Instance.completedMinigames.Contains(minigame);

    // Show the station only if it is required and unfinished.
    bool shouldShow = isRequired && !isCompleted;

    if (stationObject != null)
    {
        stationObject.SetActive(shouldShow);
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (kitchenManager != null)
        {
            kitchenManager.PlayerEnteredStation(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (kitchenManager != null)
        {
            kitchenManager.PlayerLeftStation(this);
        }
    }
}
