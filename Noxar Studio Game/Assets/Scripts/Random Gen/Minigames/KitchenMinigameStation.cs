
using UnityEngine;

public class KitchenMinigameStation : MonoBehaviour
{
    [Header("Station Information")]
    public MinigameData minigame;

    private KitchenManager kitchenManager;

    private void Start()
    {
        kitchenManager = FindFirstObjectByType<KitchenManager>();

        if (minigame == null)
        {
            Debug.LogWarning(
                "Assign a MinigameData asset to station: " + gameObject.name,
                this
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (kitchenManager != null)
            kitchenManager.PlayerEnteredStation(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (kitchenManager != null)
            kitchenManager.PlayerLeftStation(this);
    }
}
