using UnityEngine;

public class CollectionButton : MonoBehaviour
{
    public GameObject collectionPanel;
    public GameObject capsulePanel;

    public void OpenCollection()
    {
        if (collectionPanel != null)
        {
            collectionPanel.SetActive(true);
            if (capsulePanel != null)
                capsulePanel.SetActive(false);

        }
    }

    public void CloseCollection()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(false);

        capsulePanel.SetActive(true);
    }
}
