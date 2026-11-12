
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteButton : MonoBehaviour
{
    [Header("Spawner Reference")]
    public CapsuleSpawner capsuleSpawner;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check if the player tapped the screen.
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            CheckButtonTap(touchPosition);
        }

        // Also allow mouse clicks in the Unity Editor.
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition =
                Mouse.current.position.ReadValue();

            CheckButtonTap(mousePosition);
        }
    }

    private void CheckButtonTap(Vector2 screenPosition)
    {
        if (mainCamera == null)
            return;

        // Convert screen position into world position.
        Vector2 worldPosition =
            mainCamera.ScreenToWorldPoint(screenPosition);

        // Check whether the tap hit this button.
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null && hit.transform == transform)
        {
            if (capsuleSpawner != null)
            {
                capsuleSpawner.StartSpawning();
            }
        }
    }
}
