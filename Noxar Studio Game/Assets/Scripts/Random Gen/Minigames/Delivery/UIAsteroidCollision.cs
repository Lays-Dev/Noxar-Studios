
using UnityEngine;

public class UIAsteroidCollision : MonoBehaviour
{
    [Header("References")]
    public RectTransform playerRect;
    public DeliveryGameManager gameManager;

    [Header("Hitbox Sizes")]
    public Vector2 asteroidHitboxSize = new Vector2(55f, 55f);
    public Vector2 playerHitboxSize = new Vector2(60f, 60f);

    private RectTransform asteroidRect;
    private bool hasHit = false;

    private void Awake()
    {
        asteroidRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (hasHit || playerRect == null || gameManager == null)
            return;

        if (gameManager.gameEnded)
            return;

        // Get both positions in the same Canvas coordinate space.
        Vector2 asteroidPosition =
            GetCanvasPosition(asteroidRect);

        Vector2 playerPosition =
            GetCanvasPosition(playerRect);

        // Build rectangular hitboxes around each object.
        Rect asteroidBounds = new Rect(
            asteroidPosition - asteroidHitboxSize / 2f,
            asteroidHitboxSize
        );

        Rect playerBounds = new Rect(
            playerPosition - playerHitboxSize / 2f,
            playerHitboxSize
        );

        // Both horizontal AND vertical overlap are required.
        if (asteroidBounds.Overlaps(playerBounds))
        {
            hasHit = true;

            gameManager.RegisterHit();

            Destroy(gameObject);
        }
    }

    private Vector2 GetCanvasPosition(RectTransform rect)
    {
        // Convert the UI element's world position to Canvas-local space.
        RectTransform canvasRect =
            rect.GetComponentInParent<Canvas>().transform
            as RectTransform;

        return canvasRect.InverseTransformPoint(rect.position);
    }
}
