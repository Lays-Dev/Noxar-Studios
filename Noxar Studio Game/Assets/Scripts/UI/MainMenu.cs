
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Works by clicking the play button
    public void PlayGame()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ResetGame();
        }
        // Reset saved game progress
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Reset the game's time scale in case it was paused
        Time.timeScale = 1f;

        // Reload the game starting at the Counter scene
        SceneManager.LoadScene("Counter");
    }

    // Works by clicking the quit button
    public void QuitGame()
    {
        Debug.Log("Quit Pressed!");

        Application.Quit();
    }
}

