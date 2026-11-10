
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryGameManager : MonoBehaviour
{
    [Header("Hit Settings")]
    public int maxHits = 3;
    public int currentHits = 0;

    [Header("Lose Scene")]
    public string loseSceneName = "Lose";

    [Header("Star Collection")]
    public int starsCollected = 0;
    public int starsNeeded = 3;
    public string winSceneName = "Win";

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip asteroidHitSFX;
    public AudioClip starCollectedSFX;

    public bool gameEnded = false;

    public void RegisterHit()
    {
        // Ignore hits after the game ends.
        if (gameEnded)
            return;

        currentHits++;

        Debug.Log("Asteroid hits: " + currentHits);

        // Play asteroid hit sound.
        if (audioSource != null && asteroidHitSFX != null)
        {
            audioSource.PlayOneShot(asteroidHitSFX);
        }

        // Three hits ends the game.
        if (currentHits >= maxHits)
        {
            gameEnded = true;

            // Pause the main game timer.
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.PauseLevelTimer();
            }

            // Load Lose scene.
            SceneManager.LoadScene(loseSceneName);
        }
    }

    public void RegisterStar()
    {
        // Ignore collections after the game ends.
        if (gameEnded)
            return;

        starsCollected++;

        Debug.Log("Stars collected: " + starsCollected);

        // Play star collection sound.
        if (audioSource != null && starCollectedSFX != null)
        {
            audioSource.PlayOneShot(starCollectedSFX);
        }

        // Win when enough stars have been collected.
        if (starsCollected >= starsNeeded)
        {
            gameEnded = true;

            // Pause the main game timer.
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.PauseLevelTimer();

                // Calculate Galaxy Cube rewards.
                LevelManager.Instance.CalculateGalaxyCubes();
            }
            else
            {
                Debug.LogWarning("LevelManager instance is null. Cannot pause timer or calculate rewards.");
            }

            // Load Win scene.
            SceneManager.LoadScene(winSceneName);
        }
    }
}

