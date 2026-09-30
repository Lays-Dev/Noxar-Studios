using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
    }
}
