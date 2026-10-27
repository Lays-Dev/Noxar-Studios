
using UnityEngine;

public class UIStarMovement : MonoBehaviour
{
    public float moveSpeed = 400f;

    private RectTransform starRect;
    private RectTransform canvasRect;

    private void Awake()
    {
        starRect = GetComponent<RectTransform>();
        canvasRect = transform.parent as RectTransform;
    }

    private void Update()
    {
        // Move the star from right to left.
        starRect.anchoredPosition +=
            Vector2.left * moveSpeed * Time.deltaTime;

        // Destroy it after it leaves the Canvas.
        if (canvasRect != null)
        {
            float leftEdge =
                -canvasRect.rect.width / 2f -
                starRect.rect.width;

            if (starRect.anchoredPosition.x < leftEdge)
            {
                Destroy(gameObject);
            }
        }
    }
}
