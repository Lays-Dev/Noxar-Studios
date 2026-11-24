
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectionButton : MonoBehaviour
{
    public GameObject collectionPanel;
    public GameObject paymentPanel;
    public GameObject capturePanel;

    [Header("Button References")]
    public PayButton payButton;
    public CaptureButton captureButton;

    [Header("Scene Navigation")]
    public string counterSceneName = "Counter";

    public void OpenCollection()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(true);

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

        // Re-enable Capture Button.
        if (captureButton != null)
            captureButton.EnableButton();
    }

    // Travel to the Counter scene.
    public void GoToCounter()
    {
        Debug.Log("Returning to Counter scene!");

        SceneManager.LoadScene(counterSceneName);
    }
}
