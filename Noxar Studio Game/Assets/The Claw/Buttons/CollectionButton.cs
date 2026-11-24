using UnityEngine;

public class CollectionButton : MonoBehaviour
{
    public GameObject collectionPanel;
    //public GameObject capsulePanel;
    public GameObject paymentPanel;
    public GameObject capturePanel;

    [Header("Button References")]
    public PayButton payButton;
    public CaptureButton captureButton;

    public void OpenCollection()
    {
        if (collectionPanel != null)
        {
            collectionPanel.SetActive(true);
            //if (capsulePanel != null)
            //    capsulePanel.SetActive(false);

        }

        if (paymentPanel != null)
            paymentPanel.SetActive(false);
        
        if (capturePanel != null)
            capturePanel.SetActive(false);
    }

    public void CloseCollection()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(false);

        if (paymentPanel != null)
            paymentPanel.SetActive(false);

        if (capturePanel != null)
            capturePanel.SetActive(false);

        // Reset Pay Button.
        if (payButton != null)
        {
            payButton.ResetPayment();
            Debug.Log("CollectionButton: Pay button reset!");
        }
        else
        {
            Debug.LogError("CollectionButton: PayButton reference is missing!");
        }

        // Re-enable Capture Button.
        if (captureButton != null)
            captureButton.EnableButton();
    }
}
