
using UnityEngine;

public class UIStarCollection : MonoBehaviour
{
    public RectTransform playerRect;
    public DeliveryGameManager gameManager;

    private RectTransform starRect;
    private bool collected = false;

    private void Awake()
    {
        starRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (collected || playerRect == null ||
            gameManager == null)
            return;

        if (gameManager.gameEnded)
            return;

        // Get the UI bounds of the star and player.
        Vector3[] starCorners = new Vector3[4];
        Vector3[] playerCorners = new Vector3[4];

        starRect.GetWorldCorners(starCorners);
        playerRect.GetWorldCorners(playerCorners);

        Rect starBounds = GetBounds(starCorners);
        Rect playerBounds = GetBounds(playerCorners);

        // Collect the star when the UI Images overlap.
        if (starBounds.Overlaps(playerBounds))
        {
            collected = true;

            gameManager.RegisterStar();

            Destroy(gameObject);
        }
    }

    private Rect GetBounds(Vector3[] corners)
    {
        float minX = corners[0].x;
        float maxX = corners[0].x;
        float minY = corners[0].y;
        float maxY = corners[0].y;

        foreach (Vector3 corner in corners)
        {
            minX = Mathf.Min(minX, corner.x);
            maxX = Mathf.Max(maxX, corner.x);
            minY = Mathf.Min(minY, corner.y);
            maxY = Mathf.Max(maxY, corner.y);
        }

        return Rect.MinMaxRect(minX, minY, maxX, maxY);
    }
}
