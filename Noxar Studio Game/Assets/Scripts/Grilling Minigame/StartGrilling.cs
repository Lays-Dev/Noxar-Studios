
using UnityEngine;

public class StartGrilling : MonoBehaviour
{
    public GameObject grillingCanvas;
    public GameObject player;

    private bool grillingStarted = false;

    private void Start()
    {
        // Keep the grilling minigame hidden initially.
        grillingCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only respond when the player enters the collider.
        if (!other.CompareTag("Player"))
            return;

        // Prevent the grilling minigame from opening repeatedly.
        if (grillingStarted)
            return;

        // Check that the order was taken and gathering is complete.
        if (LevelManager.level2Unlocked &&
            TapSprite.level2OrderTaken &&
            EndMinigame.gatherComplete)
        {
            grillingStarted = true;
            grillingCanvas.SetActive(true);

            Debug.Log("Starting grilling minigame!");

            // Optional: disable player movement while grilling.
            // Uncomment this line if you want to stop the player.
            // player.SetActive(false);
        }
        else
        {
            Debug.Log("You need the Level 2 order and completed gathering first.");
        }
    }
}
