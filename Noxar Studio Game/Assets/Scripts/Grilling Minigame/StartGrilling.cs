/*
using UnityEngine;

public class StartGrilling : MonoBehaviour
{
    public GameObject grillingCanvas;
    public GameObject player;
    public GameObject starsGrilling;

    private bool grillingStarted = false;

    private void Start()
    {
        // Keep the grilling minigame hidden initially.
        grillingCanvas.SetActive(false);

        // Hide the stars until the requirements are met.
        starsGrilling.SetActive(false);
    }

    private void Update()
    {
        // Only activate the stars before grilling starts.
        if (!grillingStarted &&
            LevelManager.level2Unlocked &&
            TapSprite.level2OrderTaken &&
            EndMinigame.gatherComplete)
        {
            starsGrilling.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only respond when the player enters the trigger.
        if (!other.CompareTag("Player"))
            return;

        // Prevent the minigame from opening repeatedly.
        if (grillingStarted)
            return;

        // Check that all requirements are complete.
        if (LevelManager.level2Unlocked &&
            TapSprite.level2OrderTaken &&
            EndMinigame.gatherComplete)
        {
            grillingStarted = true;

            // Hide the stars and open the grilling minigame.
            starsGrilling.SetActive(false);
            grillingCanvas.SetActive(true);

            Debug.Log("Starting grilling minigame!");
        }
        else
        {
            Debug.Log(
                "You need the Level 2 order and completed gathering first."
            );
        }
    }
}
*/