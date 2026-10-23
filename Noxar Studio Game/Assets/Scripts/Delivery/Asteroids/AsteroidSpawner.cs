
using System.Collections.Generic;
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

    // Keep track of all asteroids spawned by this spawner.
    private List<GameObject> spawnedAsteroids = new List<GameObject>();

    // Prevents any more asteroids from spawning.
    private bool spawningStopped = false;

    private void Update()
    {
        // Do nothing if spawning has been stopped.
        if (spawningStopped)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnAsteroid();
            spawnTimer = 0f;
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null || spawningStopped)
            return;

        // Pick one of the five lanes randomly.
        int laneIndex = Random.Range(0, laneYPositions.Length);
        float spawnY = laneYPositions[laneIndex];

        Vector3 spawnPosition = new Vector3(
            spawnX,
            spawnY,
            0f
        );

        // Create the asteroid and remember it.
        GameObject asteroid = Instantiate(
            asteroidPrefab,
            spawnPosition,
            Quaternion.identity
        );

        spawnedAsteroids.Add(asteroid);
    }

    // Called when the player loses the delivery game.
    public void StopSpawningAndClearAsteroids()
    {
        // Stop all future spawning.
        spawningStopped = true;

        // Destroy every asteroid that still exists.
        foreach (GameObject asteroid in spawnedAsteroids)
        {
            if (asteroid != null)
            {
                Destroy(asteroid);
            }
        }

        // Clear the list.
        spawnedAsteroids.Clear();

        Debug.Log("Asteroid spawning stopped. All asteroids destroyed.");
    }
}
