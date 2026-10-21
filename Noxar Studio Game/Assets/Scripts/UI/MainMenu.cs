using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Works by clicking the play button
    public void PlayGame()
    {
        // Loads the game to the correct scene named
        SceneManager.LoadScene("Instructions");
    }

    // Works by clicking the quit button
    public void QuitGame()
    {
       // Shows this message in console to know that it worked
        Debug.Log("Quit Pressed!");

        // Exits out the game
        Application.Quit();
    }
}
