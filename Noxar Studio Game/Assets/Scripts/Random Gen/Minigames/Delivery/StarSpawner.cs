
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [Header("References")]
    public RectTransform canvasRect;
    public GameObject starPrefab;
    public RectTransform playerRect;
    public DeliveryGameManager gameManager;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;
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
        if (gameManager == null || gameManager.gameEnded)
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnStar();
            spawnTimer = 0f;
        }
    }

    private void SpawnStar()
    {
        if (canvasRect == null || starPrefab == null ||
            playerRect == null || gameManager == null)
        {
            Debug.LogWarning(
                "Assign all StarSpawner references!"
            );
            return;
        }

        if (lanePositions == null || lanePositions.Length != 5)
        {
            Debug.LogError("You need exactly five lanes!");
            return;
        }

        // Pick one of the five lanes.
        int lane = Random.Range(0, 5);

        float spawnX =
            canvasRect.rect.width / 2f + spawnOffset;

        float spawnY = lanePositions[lane];

        // Create the star under the Canvas.
        GameObject star = Instantiate(
            starPrefab,
            canvasRect
        );

        RectTransform starRect =
            star.GetComponent<RectTransform>();

        starRect.anchoredPosition =
            new Vector2(spawnX, spawnY);

        // Give the star its collection references.
        UIStarCollection collection =
            star.GetComponent<UIStarCollection>();

        if (collection != null)
        {
            collection.playerRect = playerRect;
            collection.gameManager = gameManager;
        }
        else
        {
            Debug.LogError(
                "Star prefab is missing UIStarCollection!"
            );
        }
    }
}
