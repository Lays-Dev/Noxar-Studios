
using UnityEngine;
using UnityEngine.InputSystem;

public class PayButton : MonoBehaviour
{
    [Header("Pay Button")]
    public CapsuleSpawner capsuleSpawner;
    public ClawMovement clawMovement;

    [Header("Warning Popup")]
    public GameObject warningPopup;
    public CaptureButton captureButton;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip paybuttonSFX;

    private Camera mainCamera;

    private bool hasPaid = false;
    private Collider2D buttonCollider;
    private void Awake()
    {
        mainCamera = Camera.main;
        buttonCollider = GetComponent<Collider2D>();

        // Hide warning popup when the game starts.
        if (warningPopup != null)
            warningPopup.SetActive(false);
    }

    private void Update()
    {
        CheckButtonTap();
    }

    private void CheckButtonTap()
    {
        Vector2 screenPosition;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
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

        if (hit != null && hit.GetComponentInParent<PayButton>() == this)
        {
            // Check if the player has already paid.
            if (hasPaid)
            {
                ShowWarningPopup();
                return;
            }

            // First payment.
            hasPaid = true;

            if (audioSource != null && paybuttonSFX != null)
                audioSource.PlayOneShot(paybuttonSFX);

            if (capsuleSpawner != null)
                capsuleSpawner.StartSpawning();

            if (clawMovement != null)
                clawMovement.UnlockMovement();

            Debug.Log("Payment accepted!");
        }
    }

    // Shows warning and disables both buttons.
    private void ShowWarningPopup()
    {
        if (warningPopup != null)
            warningPopup.SetActive(true);

        // Disable Pay Button.
        if (buttonCollider != null)
            buttonCollider.enabled = false;

        // Disable Capture Button.
        if (captureButton != null)
            captureButton.DisableButton();

        Debug.Log("Second payment attempted! Buttons disabled.");
    }

    // Called by MagnetClaw when a capsule is grabbed.
    public void DeactivateButton()
    {
        if (buttonCollider != null)
            buttonCollider.enabled = false;

        Debug.Log("Pay Button deactivated!");
    }

    // Re-enables the Pay Button.
    public void EnableButton()
    {
        if (buttonCollider != null)
            buttonCollider.enabled = true;

        Debug.Log("Pay Button re-enabled!");
    }

    // Resets payment so the player can pay for another batch.
    public void ResetPayment()
    {
        hasPaid = false;

        if (buttonCollider != null)
            buttonCollider.enabled = true;

        Debug.Log("Payment reset!");
    }
}
