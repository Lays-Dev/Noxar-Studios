using UnityEngine;

public class PurchaseButton : MonoBehaviour
{
    [Header("Warning Popup")]
    public GameObject warningPopup;

    [Header("Button References")]
    public PayButton payButton;
    public CaptureButton captureButton;

    // Called when the popup close button is pressed.
    public void ClosePopup()
    {
        // Close the warning popup.
        if (warningPopup != null)
            warningPopup.SetActive(false);

        // Re-enable Pay Button.
        if (payButton != null)
            payButton.EnableButton();

        // Re-enable Capture Button.
        if (captureButton != null)
            captureButton.EnableButton();

        Debug.Log("Warning popup closed!");
    }
}
