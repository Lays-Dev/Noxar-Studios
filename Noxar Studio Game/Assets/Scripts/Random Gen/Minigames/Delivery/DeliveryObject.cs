
using UnityEngine;

public class DeliveryObject : MonoBehaviour
{
    [Header("Object Type")]
    public bool isStar = false;

    [Header("Movement")]
    public float moveSpeed = 400f;
    public float destroyX = -1000f;

    private RectTransform rectTransform;
    private RectTransform playerRect;
    private DeliveryGameManager gameManager;

    private bool hasCollided = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerRect = player.GetComponent<RectTransform>();
        }

        gameManager = FindFirstObjectByType<DeliveryGameManager>();
    }

    private void Update()
    {
        if (rectTransform == null)
            return;

        // Move the UI object left.
        Vector2 position = rectTransform.anchoredPosition;
        position.x -= moveSpeed * Time.deltaTime;
        rectTransform.anchoredPosition = position;

        // Check whether the object has passed the screen.
        if (position.x < destroyX)
        {
            Destroy(gameObject);
            return;
        }

        // Check whether the object overlaps the spaceship.
        if (!hasCollided && playerRect != null &&
            RectsOverlap(rectTransform, playerRect))
        {
            hasCollided = true;

            if (gameManager != null)
            {
                if (isStar)
                    gameManager.CollectStar();
                else
                    gameManager.RegisterAsteroidHit();
            }

            Destroy(gameObject);
        }
    }

    private bool RectsOverlap(RectTransform first, RectTransform second)
    {
        // Works when both UI objects use the same Canvas.
        Rect firstRect = GetCanvasRect(first);
        Rect secondRect = GetCanvasRect(second);

        return firstRect.Overlaps(secondRect);
    }

    private Rect GetCanvasRect(RectTransform rect)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        Canvas canvas = rect.GetComponentInParent<Canvas>();
        Camera camera = null;

        if (canvas != null &&
            canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            camera = canvas.worldCamera;
        }

        Vector2 bottomLeft = RectTransformUtility.WorldToScreenPoint(
            camera, corners[0]);

        Vector2 topRight = RectTransformUtility.WorldToScreenPoint(
            camera, corners[2]);

        return Rect.MinMaxRect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x,
            topRight.y
        );
    }
}
