using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    public float speed = 1f;
    public float resetPositionX = 5.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x >= resetPositionX)
        {
            transform.position = startPosition;
        }
    }
}
