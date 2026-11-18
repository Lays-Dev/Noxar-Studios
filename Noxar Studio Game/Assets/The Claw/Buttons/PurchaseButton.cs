
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    [Header("Warning Popup")]
    public GameObject warningPopup;

    [Header("Button References")]
    public PayButton payButton;
    public CaptureButton captureButton;

    [Header("Capsule Spawner")]
    public CapsuleSpawner capsuleSpawner;

    [Header("Magnet References")]
    public MagnetClaw magnetClaw;
    [Header("Movement")]
    public ClawMovement clawMovement;
    // Called when the Purchase Button is pressed.

    public void PurchaseCapsules()
    {
        if (warningPopup != null)
            warningPopup.SetActive(false);

        // Remove the previously captured capsule.
        if (magnetClaw != null)
            magnetClaw.ResetClawMachine();

        // Reset payment.
        if (payButton != null)
            payButton.ResetPayment();

        // Enable capture button.
        if (captureButton != null)
            captureButton.EnableButton();

        // Spawn a fresh batch.
        if (capsuleSpawner != null)
            capsuleSpawner.StartNewBatch();

        // Unlock claw movement.
        if (clawMovement != null)
            clawMovement.UnlockMovement();

        Debug.Log("New capsules purchased! Claw movement unlocked.");
    }

}

