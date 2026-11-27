using UnityEngine;
using UnityEngine.InputSystem;

public class ClawMovement : MonoBehaviour
{
    [Header("Movement")]
    public InputActionReference moveAction;
    public float moveSpeed = 5f;
    public bool canMove = false;

    [Header("Movement Boundaries")]
    public float minX = -4f;
    public float maxX = 4f;

    private void OnEnable()
    {
        // Make sure the joystick movement action is enabled.
        if (moveAction != null)
        {
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        // Disable the action when this object is disabled.
        if (moveAction != null)
        {
            moveAction.action.Disable();
        }
    }

    private void Update()
    {
        // Don't move until movement is unlocked.
        if (!canMove)
            return;

        // Make sure a movement action is assigned.
        if (moveAction == null)
        {
            Debug.LogError("ClawMovement: Move Action is missing!");
            return;
        }

        // Read joystick input.
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        // Calculate horizontal movement.
        float movement = input.x * moveSpeed * Time.deltaTime;

        // Move the claw.
        Vector3 newPosition = transform.position;

        newPosition.x += movement;

        // Keep claw within boundaries.
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }

    public void UnlockMovement()
    {
        canMove = true;
        Debug.Log("ClawMovement: Movement UNLOCKED!");
    }

    public void LockMovement()
    {
        canMove = false;
        Debug.Log("ClawMovement: Movement LOCKED!");
    }
}

