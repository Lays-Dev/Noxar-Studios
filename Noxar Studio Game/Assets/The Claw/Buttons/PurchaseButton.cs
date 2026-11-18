
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

    // Called when the Purchase Button is pressed.
    public void PurchaseCapsules()
    {
        // Close the warning popup.
        if (warningPopup != null)
            warningPopup.SetActive(false);

        // Reset payment status.
        if (payButton != null)
            payButton.ResetPayment();

        // Re-enable Capture Button.
        if (captureButton != null)
            captureButton.EnableButton();

        // Spawn a fresh batch of capsules.
        if (capsuleSpawner != null)
            capsuleSpawner.StartNewBatch();

        Debug.Log("New capsules purchased!");
    }
}

