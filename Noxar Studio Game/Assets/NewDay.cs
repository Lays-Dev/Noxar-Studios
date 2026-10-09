
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewDay : MonoBehaviour
{
    public string startingSceneName = "Order Counter Blockout";
    public GameObject loseScreen;

    public void StartNewDay()
    {
        // Reset the timer
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
        }

        // Hide the lose screen
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }

        // Reload the starting scene
        SceneManager.LoadScene(startingSceneName);
    }
}

