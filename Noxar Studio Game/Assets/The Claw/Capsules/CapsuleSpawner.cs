
using UnityEngine;
using System.Collections.Generic;

public class CapsuleSpawner : MonoBehaviour
{
    [Header("Capsule Prefab")]
    public GameObject capsulePrefab;

    [Header("Available Capsules")]
    public List<CapsuleData> availableCapsules =
        new List<CapsuleData>();

    [Header("Spawn Settings")]
    public float spawnInterval = 1.5f;
    public int maxCapsules = 10;

    [Header("Capsule Container")]
    public Transform capsuleContainer;

    [Header("Spawn Area")]
    public float spawnMinX = -4f;
    public float spawnMaxX = 4f;
    public float spawnY = 6f;

    private float spawnTimer;
    private bool isSpawning = false;
    private int spawnedCapsuleCount = 0;

    private void Update()
    {
        // Only spawn after the button has been pressed.
        if (!isSpawning)
            return;

        // Stop spawning when we reach the maximum.
        if (capsuleContainer.childCount >= maxCapsules)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnCapsule();
            spawnTimer = 0f;
        }
    }

    // Call this function using your UI Button.
    public void StartSpawning()
    {
        
        if (isSpawning)
            return;

        if (capsuleContainer == null)
        {
            Debug.LogError("Capsule Container is missing!");
            return;
        }

        isSpawning = true;
        spawnTimer = 0f;

        Debug.Log("Capsule spawning activated!");
    }

    private void SpawnCapsule()
    {
        if (spawnedCapsuleCount >= maxCapsules)
            return;

        CapsuleData selectedCapsule = GetRandomCapsule();

        if (selectedCapsule == null)
            return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnMinX, spawnMaxX),
            spawnY,
            0f
        );

        GameObject newCapsule = Instantiate(
            capsulePrefab,
            spawnPosition,
            Quaternion.identity,
            capsuleContainer
        );

        Capsule capsule = newCapsule.GetComponent<Capsule>();

        if (capsule != null)
        {
            capsule.Setup(selectedCapsule);
            spawnedCapsuleCount++;
        }
        else
        {
            Destroy(newCapsule);
            Debug.LogWarning("Capsule prefab is missing the Capsule script!");
        }
    }

    private CapsuleData GetRandomCapsule()
    {
        float totalWeight = 0f;

        foreach (CapsuleData capsule in availableCapsules)
        {
            if (capsule != null)
            {
                totalWeight += Mathf.Max(0f, capsule.spawnWeight);
            }
        }

        if (totalWeight <= 0f)
            return null;

        float randomValue = Random.Range(0f, totalWeight);

        foreach (CapsuleData capsule in availableCapsules)
        {
            if (capsule == null)
                continue;

            randomValue -= Mathf.Max(0f, capsule.spawnWeight);

            if (randomValue < 0f)
            {
                return capsule;
            }
        }

        return null;
    }

    // Starts a fresh batch of capsules after a purchase.
    public void StartNewBatch()
    {
        // Stop the previous spawning process.
        isSpawning = false;

        // Destroy capsules still inside the machine.
        foreach (Transform capsule in capsuleContainer)
        {
            Destroy(capsule.gameObject);
        }

        // Reset the spawn counter.
        spawnedCapsuleCount = 0;
        spawnTimer = 0f;

        // Start spawning the new batch.
        StartSpawning();

        Debug.Log("New capsule batch purchased!");
    }
}
