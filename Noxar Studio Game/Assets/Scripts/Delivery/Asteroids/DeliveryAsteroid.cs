
using UnityEngine;

public class DeliveryAsteroid : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 3f;
    public float maxSpeed = 8f;

    [Header("Screen Boundary")]
    public float destroyX = -12f;

    private float moveSpeed;
    private bool counted = false;

    private void Start()
    {
        // Give each asteroid a random speed.
        moveSpeed = Random.Range(minSpeed, maxSpeed);

        Debug.Log("Asteroid speed: " + moveSpeed);
    }

    private void Update()
    {
        // Move the asteroid left at its chosen speed.
        transform.position +=
            Vector3.left * moveSpeed * Time.deltaTime;

        // Count it only after it safely passes the screen.
        if (!counted && transform.position.x < destroyX)
        {
            counted = true;

            DeliveryGameManager gameManager =
                FindFirstObjectByType<DeliveryGameManager>();

            if (gameManager != null)
            {
                gameManager.RegisterSafeAsteroid();
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // A collision does not count as a safe asteroid.
            DeliveryGameManager gameManager =
                FindFirstObjectByType<DeliveryGameManager>();

            if (gameManager != null)
            {
                gameManager.RegisterHit();
            }

            Destroy(gameObject);
        }
    }
}
