
using UnityEngine;

public class KitchenMinigameStation : MonoBehaviour
{
    [Header("Station Information")]
    public MinigameData minigame;

    private KitchenManager kitchenManager;

    private void Start()
    {
        kitchenManager = FindFirstObjectByType<KitchenManager>();

        Debug.Log("Station started: " + gameObject.name, this);

        if (minigame == null)
        {
            Debug.LogError(
                gameObject.name + " has no MinigameData assigned!",
                this
            );
        }
        else
        {
            Debug.Log(
                gameObject.name + " uses minigame: " +
                minigame.minigameName,
                this
            );
        }

        if (kitchenManager == null)
        {
            Debug.LogError(
                "No KitchenManager found in the scene!",
                this
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Log every object entering the trigger, even if it isn't the player.
        Debug.Log(
            gameObject.name + " detected: " +
            other.name + " | Tag: " + other.tag,
            this
        );

        if (!other.CompareTag("Player"))
        {
            Debug.Log("This object is not tagged Player.");
            return;
        }

        Debug.Log("PLAYER ENTERED STATION: " + gameObject.name);

        if (kitchenManager != null)
        {
            kitchenManager.PlayerEnteredStation(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("PLAYER LEFT STATION: " + gameObject.name);

        if (kitchenManager != null)
        {
            kitchenManager.PlayerLeftStation(this);
        }
    }
}
