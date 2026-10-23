
using UnityEngine;
using TMPro;

public class DeliveryGameManager : MonoBehaviour
{
    [Header("Player Health")]
    public int maxHits = 3;
    private int currentHits = 0;

    [Header("Delivery Goal")]
    public int asteroidsRequired = 20;
    private int safeAsteroids = 0;

    [Header("Asteroid Spawner")]
    public AsteroidSpawner asteroidSpawner;

    [Header("UI")]
    public TMP_Text hitsText;
    public TMP_Text asteroidsText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Hit Sound Effect")]
    public AudioSource audioSource;
    public AudioClip hitSFX;

    public AudioClip loseSFX;
    
    private bool gameEnded = false;

    private void Start()
    {
        // Find the spawner automatically if one isn't assigned.
        if (asteroidSpawner == null)
        {
            asteroidSpawner = FindFirstObjectByType<AsteroidSpawner>();
        }

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);

        UpdateHitsText();
        UpdateAsteroidsText();
    }

    // Called when an asteroid hits the player.
    public void RegisterHit()
    {
        if (gameEnded)
            return;

        currentHits++;

        // Play the hit sound effect once.
        if (audioSource != null && hitSFX != null)
        {
            audioSource.PlayOneShot(hitSFX);
        }

        Debug.Log("Player hit! Current hits: " + currentHits);

        UpdateHitsText();

        if (currentHits >= maxHits)
        {
            LoseGame();
        }
    }

    // Called when an asteroid safely leaves the screen.
    public void RegisterSafeAsteroid()
    {
        if (gameEnded)
            return;

        safeAsteroids++;

        Debug.Log("Safe asteroids: " + safeAsteroids);

        UpdateAsteroidsText();

        if (safeAsteroids >= asteroidsRequired)
        {
            WinGame();
        }
    }

    private void UpdateHitsText()
    {
        if (hitsText != null)
        {
            hitsText.text = "Hits: " + currentHits + " / " + maxHits;
        }
    }

    private void UpdateAsteroidsText()
    {
        if (asteroidsText != null)
        {
            asteroidsText.text =
                "Delivered: " + safeAsteroids + " / " + asteroidsRequired;
        }
    }

    public void WinGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        // Stop spawning and remove all remaining asteroids.
        StopAndClearAsteroids();

        if (winPanel != null)
            winPanel.SetActive(true);

        Debug.Log("Delivery complete! You win!");
    }

    public void LoseGame()
    {
        if (gameEnded)
            return;

        // Set this first so no more hits or deliveries can count.
        gameEnded = true;

        // Stop spawning and destroy every remaining asteroid.
        StopAndClearAsteroids();

        if (losePanel != null)
        {
            audioSource.PlayOneShot(loseSFX);
            losePanel.SetActive(true);
        }

        Debug.Log("Delivery failed! You lose!");
    }

    private void StopAndClearAsteroids()
    {
        if (asteroidSpawner != null)
        {
            asteroidSpawner.StopSpawningAndClearAsteroids();
        }
    }
}
