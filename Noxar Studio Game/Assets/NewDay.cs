using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDay : MonoBehaviour
{
    public string startingSceneName = "Order Counter Blockout";

    public GameObject loseScreen;
    public GameObject winScreen;

    public void StartNewDay()
    {
        // Reset the timer
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
        }

        // Reset patron order status
        TapSprite.patronHasOrdered = false;

        // Reset gather status
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