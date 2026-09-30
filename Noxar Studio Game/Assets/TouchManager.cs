using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    // PlayerInput action map
    [Tooltip("Put the player Input Action Map here")]
    [SerializeField] private InputActionAsset inputActions;
    [Tooltip("Put the player gameObject here")]
    [SerializeField] private Transform player;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    private void Awake()
    {
        touchPositionAction = inputActions.FindAction("TouchPosition");
        touchPressAction = inputActions.FindAction("TouchPress");
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
        touchPressAction.Enable();
        touchPositionAction.Enable();
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        touchPressAction.Disable();
        touchPositionAction.Disable();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        worldPosition.z = player.position.z;
        player.position = worldPosition;

        Debug.Log("Screen tapped at: " + worldPosition);
    }
}