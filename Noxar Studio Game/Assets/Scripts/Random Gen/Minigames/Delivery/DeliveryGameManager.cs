
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeliveryGameManager : MonoBehaviour
{
    [Header("Win and Lose")]
    public int starsNeededToWin = 3;
    public int maxAsteroidHits = 3;

    public string winSceneName = "Win";
    public string loseSceneName = "Lose";

    [Header("UI Counters")]
    public TMP_Text starCounterText;
    public TMP_Text hitCounterText;

    private int starsCollected = 0;
    private int asteroidHits = 0;
    private bool gameEnded = false;

    private void Start()
    {
        UpdateUI();
    }

    public void CollectStar()
    {
        if (gameEnded)
            return;

        starsCollected++;
        UpdateUI();

        Debug.Log("Stars collected: " + starsCollected);

        if (starsCollected >= starsNeededToWin)
        {
            gameEnded = true;
            Debug.Log("Delivery successful!");
            SceneManager.LoadScene(winSceneName);
        }
    }

    public void RegisterAsteroidHit()
    {
        if (gameEnded)
            return;

        asteroidHits++;
        UpdateUI();

        Debug.Log("Asteroid hits: " + asteroidHits);

        if (asteroidHits >= maxAsteroidHits)
        {
            gameEnded = true;
            Debug.Log("Delivery failed!");
            SceneManager.LoadScene(loseSceneName);
        }
    }

    private void UpdateUI()
    {
        if (starCounterText != null)
        {
            starCounterText.text =
                "Stars: " + starsCollected + " / " + starsNeededToWin;
        }

        if (hitCounterText != null)
        {
            hitCounterText.text =
                "Hits: " + asteroidHits + " / " + maxAsteroidHits;
        }
    }

    public bool IsGameEnded()
    {
        return gameEnded;
    }
}
