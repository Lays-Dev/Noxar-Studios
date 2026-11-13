using System;
using UnityEngine;

public class KitchenMinigameStation : MonoBehaviour
{
    [Header("Station Information")]
    public MinigameData minigame;

    [Header("Station Visibility")]
    public GameObject stationObject;

    [Header("Manager Reference")]
    public KitchenManager kitchenManager;

    [Header("Audio")]
    public AudioSource audioSource;

    [Serializable]
    public class MinigameSound
    {
        public string minigameName;
        public AudioClip soundEffect;
    }

    [Header("Minigame Sounds")]
    public MinigameSound[] minigameSounds;



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

    // Automatically find the audio source if not assigned.
    if (audioSource == null)
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError(
                "KitchenMinigameStation: No AudioSource found on " +
                gameObject.name + "!",
                this
            );
        }
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


// Play the sound effect only if this station's minigame
// was selected for the current level.
private void PlayStationSFX()
{
    Debug.Log("PlayStationSFX was called!", this);

    // Check that a minigame is assigned.
    if (minigame == null)
    {
        Debug.LogWarning(
            "No minigame assigned to station " + gameObject.name,
            this
        );
        return;
    }

    // Check that the LevelManager exists.
    if (LevelManager.Instance == null)
    {
        Debug.LogWarning("LevelManager instance not found!", this);
        return;
    }

    // Check whether this minigame was selected for this level.
    if (!LevelManager.Instance.requiredMinigames.Contains(minigame))
    {
        Debug.Log(
            "No SFX played. " + minigame.minigameName +
            " was not selected for this level.",
            this
        );
        return;
    }

    // Check that an AudioSource is assigned.
    if (audioSource == null)
    {
        Debug.LogWarning(
            "No AudioSource found on station " + gameObject.name,
            this
        );
        return;
    }

    // Find and play the matching sound effect.
    foreach (MinigameSound sound in minigameSounds)
    {
        if (sound == null || sound.soundEffect == null)
        {
            continue;
        }

        if (string.Equals(
            sound.minigameName,
            minigame.minigameName,
            StringComparison.OrdinalIgnoreCase))
        {
            audioSource.PlayOneShot(sound.soundEffect);

            Debug.Log(
                "Playing sound effect for selected minigame: " +
                minigame.minigameName,
                this
            );

            break;
        }
    }
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
        Debug.Log("Trigger detected: " + other.name, this);

        if (!other.CompareTag("Player"))
        {
            Debug.Log("Object is not tagged Player: " + other.name);
            return;
        }

        Debug.Log("PLAYER ENTERED STATION: " + name, this);

        // IMPORTANT: Play the matching minigame sound.
        PlayStationSFX();

        // Open the minigame as usual.
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
