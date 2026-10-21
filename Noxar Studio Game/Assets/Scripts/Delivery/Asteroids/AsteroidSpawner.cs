using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [Header("Asteroid")]
    public GameObject asteroidPrefab;

    [Header("Spawn Settings")]
    public float spawnX = 10f;
    public float spawnInterval = 2f;

    [Header("Five Lane Positions")]
    public float[] laneYPositions = { -4f, -2f, 0f, 2f, 4f };

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
        if (asteroidPrefab == null)
            return;

        // Pick one of the five lanes randomly.
        int laneIndex = Random.Range(
            0, laneYPositions.Length
        );

        float spawnY = laneYPositions[laneIndex];

        Vector3 spawnPosition = new Vector3(
            spawnX,
            spawnY,
            0f
        );

        // Create the asteroid.
        Instantiate(
            asteroidPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}

