
using UnityEngine;
using UnityEngine.InputSystem;

public class CaptureButton : MonoBehaviour
{
    [Header("Magnet References")]
    public MagnetClaw magnetClaw;
    public ClawMovement clawMovement;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip captureButtonSFX;

    private Camera mainCamera;
    private Collider2D buttonCollider;

    private void Awake()
    {
        mainCamera = Camera.main;
        buttonCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckButtonTap();
    }

    private void CheckButtonTap()
    {
        Vector2 screenPosition;

        // Check for touchscreen input.
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // Check for mouse input in the Editor.
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        Vector2 worldPosition =
            mainCamera.ScreenToWorldPoint(screenPosition);

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null && hit.GetComponentInParent<CaptureButton>() == this)
        {
            // Play capture button sound.
            if (audioSource != null && captureButtonSFX != null)
                audioSource.PlayOneShot(captureButtonSFX);

            // Lock claw movement.
            if (clawMovement != null)
                clawMovement.LockMovement();

            // Activate magnet.
            if (magnetClaw != null)
                magnetClaw.MagnetizeClosestCapsule();

            Debug.Log("Magnet button pressed!");
        }
    }
    public void DisableButton()
    {
        if (buttonCollider != null)
            buttonCollider.enabled = false;

        Debug.Log("Magnet Button disabled!");
    }
    // Re-enables the Capture Button.
    public void EnableButton()
    {
        if (buttonCollider != null)
            buttonCollider.enabled = true;

        Debug.Log("Capture Button re-enabled!");
    }
}
