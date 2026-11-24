
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionUI : MonoBehaviour
{
    [Header("Collection References")]
    public Transform contentParent;
    public ItemCollectionEntry itemEntryPrefab;

    [Header("Selected Item Preview")]
    public Image selectedItemImage;

    [Header("Equip Button")]
    public GameObject equipButton;

    private bool isListening;
    private ItemData selectedItem;

    private void OnEnable()
    {
        // Hide Equip button when collection opens.
        if (equipButton != null)
            equipButton.SetActive(false);

        TryConnectToManager();
    }

    private void Update()
    {
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
            return;

        if (contentParent == null || itemEntryPrefab == null)
        {
            Debug.LogWarning("Collection UI references are missing!");
            return;
        }

        // Remove old entries.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var allItems =
            ItemCollectionManager.Instance.GetAllItems();

        // Create an entry for every cosmetic.
        foreach (ItemData item in allItems)
        {
            if (item == null)
                continue;

            ItemCollectionEntry newEntry =
                Instantiate(itemEntryPrefab, contentParent);

            newEntry.Setup(item, this);
        }
    }

    // Displays the selected cosmetic on the left.
    public void ShowSelectedItem(ItemData item)
    {
        if (item == null)
            return;

        // Remember which cosmetic was selected.
        selectedItem = item;

        // Display the enlarged cosmetic image.
        if (selectedItemImage != null)
        {
            selectedItemImage.sprite = item.itemSprite;

            selectedItemImage.color = item.isUnlocked
                ? Color.white
                : Color.black;
        }

        // Only show Equip button for unlocked cosmetics.
        if (equipButton != null)
        {
            equipButton.SetActive(item.isUnlocked);
        }

        Debug.Log("Selected cosmetic: " + item.itemName);
    }

    public void EquipSelectedItem()
    {
        if (selectedItem == null)
            return;

        if (!selectedItem.isUnlocked)
            return;

        // Equip this specific cosmetic.
        selectedItem.isEquipped = true;

        Debug.Log(selectedItem.itemName + " equipped!");
    }
}
