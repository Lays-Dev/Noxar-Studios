
using UnityEngine;

public class KitchenMinigameStation : MonoBehaviour
{
    [Header("Station Information")]
    public MinigameData minigame;

    [Header("Station Visibility")]
    public GameObject stationObject;

[Header("Manager Reference")]
public KitchenManager kitchenManager;

private void Start()
{
    
    if (kitchenManager == null)
    {
        Debug.LogError(
            "KitchenMinigameStation: No active KitchenManager found!",
            this
        );
    }
    else
    {
        Debug.Log(
            "KitchenManager successfully connected to " + gameObject.name,
            this
        );
    }

    // Check the station's minigame assignment.
    if (minigame == null)
    {
        Debug.LogError(
            gameObject.name + " has no MinigameData assigned!",
            this
        );
    }
    else
    {
        Debug.Log("Station uses minigame: " + minigame.minigameName);
    }

    UpdateStationVisibility();
}



public void UpdateStationVisibility()
{
    if (minigame == null || LevelManager.Instance == null)
        return;

    LevelManager levelManager = LevelManager.Instance;

    // Is this minigame required for the current order?
    bool isRequired =
        levelManager.requiredMinigames.Contains(minigame);

    // Has this minigame already been completed?
    bool isCompleted =
        levelManager.completedMinigames.Contains(minigame);

    // Is this the Delivery minigame?
    bool isDeliveryMinigame =
        minigame == levelManager.deliveryMinigame;

    // Delivery must remain locked until the other minigames
    // are completed.
    bool deliveryIsLocked =
        isDeliveryMinigame &&
        !levelManager.deliveryMinigameUnlocked;

    // Only show a required, unfinished, unlocked station.
    bool shouldShow =
        isRequired &&
        !isCompleted &&
        !deliveryIsLocked;

    if (stationObject != null)
    {
        stationObject.SetActive(shouldShow);
    }
}



private void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log(
        name + " detected object: " + other.name,
        this
    );

    Debug.Log(
        "Object tag: " + other.tag,
        this
    );

    if (!other.CompareTag("Player"))
    {
        Debug.Log("Detected object is not tagged Player.");
        return;
    }

    Debug.Log("PLAYER ENTERED STATION: " + name);

    if (kitchenManager != null)
    {
        kitchenManager.PlayerEnteredStation(this);
    }
    else
    {
        Debug.LogError("KitchenManager reference is missing!", this);
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
