
using UnityEngine;

public class CapsuleCollectionUI2 : MonoBehaviour
{
    [Header("Collection Screen")]
    public GameObject collectionPanel;

    [Header("Entry Settings")]
    public GameObject capsuleEntryPrefab;
    public Transform contentParent;

    [Header("Capture Screen")]
    public GameObject capturePanel;

    private void OnEnable()
    {
        // Listen for changes to the capsule collection.
        if (CapsuleCollectionManager.Instance != null)
        {
            CapsuleCollectionManager.Instance.OnCollectionChanged
                += RefreshCollection;
        }

        RefreshCollection();
    }

    private void OnDisable()
    {
        // Stop listening when this UI object is disabled.
        if (CapsuleCollectionManager.Instance != null)
        {
            CapsuleCollectionManager.Instance.OnCollectionChanged
                -= RefreshCollection;
        }
    }

    public void OpenCollection()
    {
        if (collectionPanel != null)
        {
            collectionPanel.SetActive(true);
            capturePanel.SetActive(false);
            RefreshCollection();
        }
    }

    public void CloseCollection()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(false);
    }

    public void RefreshCollection()
    {
        if (CapsuleCollectionManager.Instance == null)
            return;

        if (capsuleEntryPrefab == null || contentParent == null)
        {
            Debug.LogWarning("Collection UI references are missing!");
            return;
        }

        // Remove the old entries.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Get all possible capsule types.
        foreach (CapsuleData capsuleData in
                 CapsuleCollectionManager.Instance.allCapsules)
        {
            if (capsuleData == null)
                continue;

            // Get how many of this capsule have been collected.
            int amount =
                CapsuleCollectionManager.Instance.GetCapsuleCount(capsuleData);

            // Create a new UI entry.
            GameObject newEntry = Instantiate(
                capsuleEntryPrefab,
                contentParent
            );

            // Set up the entry.
            CapsuleCollectionEntry entry =
                newEntry.GetComponent<CapsuleCollectionEntry>();

            if (entry != null)
            {
                entry.Setup(capsuleData, amount);
            }
            else
            {
                Debug.LogWarning(
                    "CapsuleEntry prefab is missing CapsuleCollectionEntry!"
                );
            }
        }
    }
}
