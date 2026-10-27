
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("References")]
    public RectTransform canvasRect;
    public GameObject asteroidPrefab;

    [Header("Collision References")]
    public RectTransform playerRect;
    public DeliveryGameManager gameManager;

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

        // Pick a random lane from 0 to 4.
        int randomLane = Random.Range(0, 5);

        // Spawn just beyond the right edge of the Canvas.
        float spawnX =
            canvasRect.rect.width / 2f + spawnOffset;

        float spawnY = lanePositions[randomLane];

        // Create the asteroid as a child of the Canvas.
        GameObject asteroid = Instantiate(
            asteroidPrefab,
            canvasRect
        );

        RectTransform asteroidRect =
            asteroid.GetComponent<RectTransform>();


        UIAsteroidCollision collision =
            asteroid.GetComponent<UIAsteroidCollision>();

        collision.playerRect = playerRect;
        collision.gameManager = gameManager;


        // Position the asteroid in its selected lane.
        asteroidRect.anchoredPosition =
            new Vector2(spawnX, spawnY);
    }
}
