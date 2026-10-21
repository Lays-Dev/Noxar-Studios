using UnityEngine;

public class DeliveryAsteroid : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private void Update()
    {
        // Move the asteroid from right to left.
        transform.Translate(
            Vector3.left * moveSpeed * Time.deltaTime
        );

        // Destroy it after it leaves the screen.
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Asteroid touched: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected by asteroid!");

            DeliveryGameManager gameManager =
                FindFirstObjectByType<DeliveryGameManager>();

            if (gameManager != null)
            {
                gameManager.RegisterHit();
            }
            else
            {
                Debug.LogError("DeliveryGameManager was not found!");
            }

            Destroy(gameObject);
        }
    }


}
