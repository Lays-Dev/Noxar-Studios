using UnityEngine;

public class Level2Win : MonoBehaviour
{
    public GameObject winScreenCanvas;

    public void ShowWinScreen()
    {
        // Show the win screen.
        winScreenCanvas.SetActive(true);

        // Pause the game.
        Time.timeScale = 0f;
    }
}
