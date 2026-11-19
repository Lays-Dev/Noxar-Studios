using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject collectionPanel;

    [Header("Button References")]
    public PayButton payButton;
    public CaptureButton captureButton;

    public void CloseCollection()
    {
                // Re-enable Pay Button.
        if (payButton != null)
            payButton.EnableButton();

        // Re-enable Capture Button.
        if (captureButton != null)
            captureButton.EnableButton();

        if (collectionPanel != null)
            collectionPanel.SetActive(false);
    }
}
