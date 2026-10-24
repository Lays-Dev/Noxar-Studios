using UnityEngine;

public class DeliveryBackground : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 1f;

    public float startX = -13f;
    public float endX = 13f;

    private void Update()
    {
        // Move the background slowly to the right.
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        // Stop when the background reaches X = 13.
        if (transform.position.x >= endX)
        {
            transform.position = new Vector3(
                endX,
                transform.position.y,
                transform.position.z
            );

            enabled = false;
        }
    }

    private void Start()
    {
        // Set the starting position.
        transform.position = new Vector3(
            startX,
            transform.position.y,
            transform.position.z
        );
    }
}
