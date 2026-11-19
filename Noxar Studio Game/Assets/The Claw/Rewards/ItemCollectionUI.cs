using UnityEngine;

public class ItemCollectionUI : MonoBehaviour
{
    [Header("Collection Page")]
    public GameObject collectionPanel;

    [Header("Item Entries")]
    public GameObject itemEntryPrefab;
    public Transform contentParent;

    private void OnEnable()
    {
        SubscribeToCollection();
        RefreshCollection();
    }

    private void Start()
    {
        // Try again after all scene objects have initialized.
        SubscribeToCollection();
        RefreshCollection();
    }

    private void OnDisable()
    {
        if (ItemCollectionManager.Instance != null)
        {
            ItemCollectionManager.Instance.OnItemCollectionChanged
                -= RefreshCollection;
        }
    }

    private void SubscribeToCollection()
    {
        if (ItemCollectionManager.Instance != null)
        {
            // Remove first to prevent duplicate subscriptions.
            ItemCollectionManager.Instance.OnItemCollectionChanged
                -= RefreshCollection;

            ItemCollectionManager.Instance.OnItemCollectionChanged
                += RefreshCollection;
        }
    }

    public void OpenCollection()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(true);

        RefreshCollection();
    }

    public void CloseCollection()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(false);
    }

    public void RefreshCollection()
    {
        if (ItemCollectionManager.Instance == null)
            return;

        if (itemEntryPrefab == null || contentParent == null)
        {
            Debug.LogWarning("Item collection UI references are missing!");
            return;
        }

        // Remove the old entries.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Display every possible item.
        foreach (ItemData item in ItemCollectionManager.Instance.allItems)
        {
            if (item == null)
                continue;

            bool unlocked =
                ItemCollectionManager.Instance.IsItemUnlocked(item);

            // Create an entry for this item.
            GameObject newEntry = Instantiate(
                itemEntryPrefab,
                contentParent
            );

            ItemCollectionEntry entry =
                newEntry.GetComponent<ItemCollectionEntry>();

            if (entry != null)
            {
                entry.Setup(item, unlocked);
            }
        }
    }
}
