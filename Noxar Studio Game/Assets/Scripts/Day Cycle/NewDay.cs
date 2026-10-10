using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDay : MonoBehaviour
{
    public string startingSceneName = "Order Counter Blockout";

    public GameObject loseScreen;
    public GameObject winScreen;

    // Check which screen the button belongs to
    public bool comingFromWinScreen = false;

    public static bool level2 = false;

    public void StartNewDay()
    {
        // Only advance to level 2 from the win screen
        if (comingFromWinScreen && !level2)
        {
            level2 = true;
            Debug.Log("Level 2 activated!");
        }

        Debug.Log("Level 2 is: " + level2);

        // Reset the timer
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
        }

        // Reset patron order status
        TapSprite.patronHasOrdered = false;

        // Reset gathering status
        EndMinigame.gatherComplete = false;

        // Hide the lose screen
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }

        // Hide the win screen
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }

        // Reload the starting scene
        SceneManager.LoadScene(startingSceneName);
    }
}