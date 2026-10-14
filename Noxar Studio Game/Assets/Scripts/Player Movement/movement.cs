using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Connecting the Input Action 
    [SerializeField] private InputActionReference moveAction;
    // Players Rigidbody2D component, used for physics-based movement
    private Rigidbody2D rb;
    // Stores the joystick input for movement, which can be used by other scripts like PlayerAnimations
    public Vector2 MoveInput { get; private set; }
    // Get the players Rigidbody component
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Enable the Input Action when the script is enabled
    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    // Disable the Input Action when the script is disabled
    private void OnDisable()
    {
        moveAction.action.Disable();
    }


    private void Update()
    {
        // Read the joystick direction
        MoveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Move the player using physics
        Vector2 movement = MoveInput * moveSpeed;

        rb.MovePosition(
            rb.position + movement * Time.fixedDeltaTime
        );
    }
}
