
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    [Header("Purchase Settings")]
    public int purchaseCost = 50;

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

    public void PurchaseCapsules()
    {
        // Find the LevelManager.
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager not found!");
            return;
        }

        // Check if the player can afford the purchase.
        if (LevelManager.Instance.totalGalaxyCubes < purchaseCost)
        {
            Debug.Log("Not enough Galaxy Cubes!");

            if (warningPopup != null)
                warningPopup.SetActive(false);

            return;
        }

        // Deduct the purchase cost.
        LevelManager.Instance.totalGalaxyCubes -= purchaseCost;

        // Save the updated balance.
        LevelManager.Instance.SaveGalaxyCubes();

        // Close the confirmation popup.
        if (warningPopup != null)
            warningPopup.SetActive(false);

        // Remove the previously captured capsule.
        if (magnetClaw != null)
            magnetClaw.ResetClawMachine();

        // Enable the capture button.
        if (captureButton != null)
            captureButton.EnableButton();

        // Spawn a fresh batch.
        if (capsuleSpawner != null)
            capsuleSpawner.StartNewBatch();

        // Unlock claw movement.
        if (clawMovement != null)
            clawMovement.UnlockMovement();

        // Re-enable the Pay Button.
        if (payButton != null)
            payButton.EnableButton();

        Debug.Log(
            "Purchase successful! -" + purchaseCost +
            " Galaxy Cubes. Remaining: " +
            LevelManager.Instance.totalGalaxyCubes
        );
    }
}
