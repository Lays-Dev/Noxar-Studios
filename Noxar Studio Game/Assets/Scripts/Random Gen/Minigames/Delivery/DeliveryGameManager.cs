
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryGameManager : MonoBehaviour
{
    [Header("Hit Settings")]
    public int maxHits = 3;
    public int currentHits = 0;

    [Header("Lose Scene")]
    public string loseSceneName = "Lose";

    public bool gameEnded = false;

    public void RegisterHit()
    {
        // Ignore any hits after the game ends.
        if (gameEnded)
            return;

        currentHits++;

        Debug.Log("Asteroid hits: " + currentHits);

        // Three hits ends the game.
        if (currentHits >= maxHits)
        {
            gameEnded = true;
            SceneManager.LoadScene(loseSceneName);
        }
    }

    
[Header("Star Collection")]
public int starsCollected = 0;
public int starsNeeded = 3;
public string winSceneName = "Win";

public void RegisterStar()
{
    // Do nothing if the game has already ended.
    if (gameEnded)
        return;

    starsCollected++;

    Debug.Log("Stars collected: " + starsCollected);

    // Win when three stars have been collected.
    if (starsCollected >= starsNeeded)
    {
        gameEnded = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene(winSceneName);
    }
}

}
