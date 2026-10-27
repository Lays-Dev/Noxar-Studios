
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
