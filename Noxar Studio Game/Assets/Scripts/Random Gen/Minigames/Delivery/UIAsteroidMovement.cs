
using UnityEngine;

public class UIAsteroidMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 400f;

    private RectTransform asteroidRect;
    private RectTransform canvasRect;

    private void Awake()
    {
        asteroidRect = GetComponent<RectTransform>();

        // Find the Canvas this asteroid belongs to.
        canvasRect = transform.parent as RectTransform;
    }

    private void Update()
    {
        // Move the asteroid from right to left.
        asteroidRect.anchoredPosition +=
            Vector2.left * moveSpeed * Time.deltaTime;

        // Destroy it once it has moved off the left side.
        float leftEdge =
            -canvasRect.rect.width / 2f -
            asteroidRect.rect.width;

        if (asteroidRect.anchoredPosition.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
