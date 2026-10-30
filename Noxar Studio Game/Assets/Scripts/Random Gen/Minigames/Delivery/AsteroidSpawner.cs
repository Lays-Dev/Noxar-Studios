
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("References")]
    public RectTransform canvasRect;
    public GameObject asteroidPrefab;

    [Header("Collision References")]
    public RectTransform playerRect;
    public DeliveryGameManager gameManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip asteroidSpawnSFX;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnOffset = 100f;

    [Header("Five Lane Y Positions")]
    public float[] lanePositions =
    {
        351f,
        170f,
        0f,
        -170f,
        -351f
    };

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnAsteroid();
            spawnTimer = 0f;
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null || canvasRect == null)
        {
            Debug.LogWarning(
                "Assign the Asteroid Prefab and Canvas Rect!"
            );
            return;
        }

        if (lanePositions == null || lanePositions.Length != 5)
        {
            Debug.LogError("You need exactly five lanes!");
            return;
        }

        // Pick a random lane.
        int randomLane = Random.Range(0, 5);

        // Spawn beyond the right edge of the Canvas.
        float spawnX =
            canvasRect.rect.width / 2f + spawnOffset;

        float spawnY = lanePositions[randomLane];

        // Create the asteroid.
        GameObject asteroid = Instantiate(
            asteroidPrefab,
            canvasRect
        );

        UIAsteroidCollision collision =
            asteroid.GetComponent<UIAsteroidCollision>();

        collision.playerRect = playerRect;
        collision.gameManager = gameManager;

        // Position the asteroid.
        RectTransform asteroidRect =
            asteroid.GetComponent<RectTransform>();

        asteroidRect.anchoredPosition =
            new Vector2(spawnX, spawnY);

        // Play spawn sound.
        if (audioSource != null && asteroidSpawnSFX != null)
        {
            audioSource.PlayOneShot(asteroidSpawnSFX);
        }
    }
}

