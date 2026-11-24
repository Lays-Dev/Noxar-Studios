
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

        // Re-enable Pay Button.
        if (payButton != null)
        {
            payButton.EnableButton();
            Debug.Log("Pay Button re-enabled!");
        }

        // Re-enable Capture Button.
        if (captureButton != null)
            captureButton.EnableButton();
    }

    public void GoToCounter()
    {
        Debug.Log("Returning to Counter scene!");

        SceneManager.LoadScene(counterSceneName);
    }
}
