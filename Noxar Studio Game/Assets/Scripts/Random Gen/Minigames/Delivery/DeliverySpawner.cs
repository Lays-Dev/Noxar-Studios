
using UnityEngine;

public class DeliverySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject asteroidPrefab;
    public GameObject starPrefab;

    [Header("Five Lane Y Positions")]
    public float[] lanePositions = { 351f, 170f, 0f, -170f, -351f };

    [Header("Spawn Position")]
    public float spawnX = 12f;
    public float destroyX = -12f;

    [Header("Asteroid Spawning")]
    public float asteroidMinSpawnTime = 1f;
    public float asteroidMaxSpawnTime = 2f;
    public float asteroidMinSpeed = 4f;
    public float asteroidMaxSpeed = 8f;

    [Header("Star Spawning")]
    public float starMinSpawnTime = 4f;
    public float starMaxSpawnTime = 6f;
    public float starSpeed = 5f;

    private float asteroidTimer;
    private float starTimer;

    private DeliveryGameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<DeliveryGameManager>();

        if (lanePositions == null || lanePositions.Length != 5)
        {
            Debug.LogError("DeliverySpawner needs exactly five lane positions!");
            enabled = false;
            return;
        }

        // Randomize the first spawn times.
        asteroidTimer = Random.Range(
            asteroidMinSpawnTime,
            asteroidMaxSpawnTime
        );

        starTimer = Random.Range(
            starMinSpawnTime,
            starMaxSpawnTime
        );
    }

    private void Update()
    {
        if (gameManager != null && gameManager.IsGameEnded())
            return;

        // Spawn asteroids.
        asteroidTimer -= Time.deltaTime;

        if (asteroidTimer <= 0f)
        {
            SpawnAsteroid();

            asteroidTimer = Random.Range(
                asteroidMinSpawnTime,
                asteroidMaxSpawnTime
            );
        }

        // Spawn stars less frequently.
        starTimer -= Time.deltaTime;

        if (starTimer <= 0f)
        {
            SpawnStar();

            starTimer = Random.Range(
                starMinSpawnTime,
                starMaxSpawnTime
            );
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null)
            return;

        int lane = Random.Range(0, 5);

        Vector3 spawnPosition = new Vector3(
            spawnX,
            lanePositions[lane],
            0f
        );

        GameObject asteroid = Instantiate(
            asteroidPrefab,
            spawnPosition,
            Quaternion.identity
        );

        DeliveryObject deliveryObject =
            asteroid.GetComponent<DeliveryObject>();

        if (deliveryObject != null)
        {
            // Every asteroid can have a different speed.
            deliveryObject.moveSpeed = Random.Range(
                asteroidMinSpeed,
                asteroidMaxSpeed
            );

            deliveryObject.destroyX = destroyX;
            deliveryObject.isStar = false;
        }
    }

    private void SpawnStar()
    {
        if (starPrefab == null)
            return;

        int lane = Random.Range(0, 5);

        Vector3 spawnPosition = new Vector3(
            spawnX,
            lanePositions[lane],
            0f
        );

        GameObject star = Instantiate(
            starPrefab,
            spawnPosition,
            Quaternion.identity
        );

        DeliveryObject deliveryObject =
            star.GetComponent<DeliveryObject>();

        if (deliveryObject != null)
        {
            // Every star uses the same speed.
            deliveryObject.moveSpeed = starSpeed;
            deliveryObject.destroyX = destroyX;
            deliveryObject.isStar = true;
        }
    }
}
