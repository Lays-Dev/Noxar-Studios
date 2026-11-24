
using UnityEngine;

public class ItemCollectionUI : MonoBehaviour
{
    [Header("Collection References")]
    public Transform contentParent;
    public ItemCollectionEntry itemEntryPrefab;

    private bool isListening;

    private void OnEnable()
    {
        TryConnectToManager();
    }

    private void Update()
    {
        // Keep checking until the manager exists.
        if (!isListening)
        {
            TryConnectToManager();
        }
    }

    private void OnDisable()
    {
        DisconnectFromManager();
    }

    private void TryConnectToManager()
    {
        if (ItemCollectionManager.Instance == null)
            return;

        // Avoid subscribing multiple times.
        if (isListening)
            return;

        ItemCollectionManager.Instance.OnItemCollectionChanged
            += RefreshCollection;

        isListening = true;

        Debug.Log("Connected to ItemCollectionManager!");

        RefreshCollection();
    }

    private void DisconnectFromManager()
    {
        if (ItemCollectionManager.Instance != null && isListening)
        {
            ItemCollectionManager.Instance.OnItemCollectionChanged
                -= RefreshCollection;
        }

        isListening = false;
    }

    public void RefreshCollection()
    {
        if (ItemCollectionManager.Instance == null)
        {
            Debug.LogWarning("ItemCollectionManager not found!");
            return;
        }

        if (contentParent == null || itemEntryPrefab == null)
        {
            Debug.LogWarning("Collection UI references are missing!");
            return;
        }

        // Destroy old entries.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Get all cosmetics.
        var allItems =
            ItemCollectionManager.Instance.GetAllItems();

        // Create a UI entry for each cosmetic.
        foreach (ItemData item in allItems)
        {
            if (item == null)
                continue;

            ItemCollectionEntry newEntry =
                Instantiate(itemEntryPrefab, contentParent);

            newEntry.Setup(item);
        }

        Debug.Log("Cosmetic collection refreshed!");
    }
}
