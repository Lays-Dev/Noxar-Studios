using UnityEngine;
using UnityEngine.InputSystem;

public class ClawMovement : MonoBehaviour
{
    [Header("Movement")]
    public InputActionReference moveAction;
    public float moveSpeed = 5f;

    [Header("Movement Boundaries")]
    public float minX = -4f;
    public float maxX = 4f;

    private void Update()
    {
        if (moveAction == null)
            return;

        // Read horizontal joystick input.
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Move left or right.
        float movement = input.x * moveSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position;

        newPosition.x += movement;

        // Keep the claw inside the machine.
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }
}

