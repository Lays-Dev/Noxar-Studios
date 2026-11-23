
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ItemInventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    public GameObject inventoryPanel;
    public Transform contentParent;
    public GameObject itemEntryPrefab;

    [Header("Page Settings")]
    public int itemsPerPage = 12;
    public TMP_Text pageText;
    public Button previousPageButton;
    public Button nextPageButton;

    [Header("Item Details")]
    public Image selectedItemImage;
    public TMP_Text selectedItemName;
    public TMP_Text selectedItemDescription;
    public TMP_Text selectedItemRarity;
    public TMP_Text selectedItemSlot;
    public TMP_Text selectedItemStatus;

    [Header("Equipment Buttons")]
    public Button equipButton;
    public Button unequipButton;

    private List<ItemData> allItems = new List<ItemData>();

    private int currentPage = 0;
    private ItemData selectedItem;

    private void OnEnable()
    {
        SubscribeToEvents();
        RefreshInventory();
    }

    private void OnDisable()
    {
        if (ItemCollectionManager.Instance != null)
        {
            ItemCollectionManager.Instance.OnItemCollectionChanged
                -= RefreshInventory;
        }

        if (ItemEquipmentManager.Instance != null)
        {
            ItemEquipmentManager.Instance.OnEquipmentChanged
                -= RefreshSelectedItem;
        }
    }

    private void SubscribeToEvents()
    {
        if (ItemCollectionManager.Instance != null)
        {
            ItemCollectionManager.Instance.OnItemCollectionChanged
                -= RefreshInventory;

            ItemCollectionManager.Instance.OnItemCollectionChanged
                += RefreshInventory;
        }

        if (ItemEquipmentManager.Instance != null)
        {
            ItemEquipmentManager.Instance.OnEquipmentChanged
                -= RefreshSelectedItem;

            ItemEquipmentManager.Instance.OnEquipmentChanged
                += RefreshSelectedItem;
        }
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        currentPage = 0;
        RefreshInventory();
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void RefreshInventory()
    {
        if (ItemCollectionManager.Instance == null)
            return;

        if (contentParent == null || itemEntryPrefab == null)
            return;

        allItems = ItemCollectionManager.Instance.GetAllItems();

        int totalPages = Mathf.Max(
            1,
            Mathf.CeilToInt((float)allItems.Count / itemsPerPage)
        );

        currentPage = Mathf.Clamp(currentPage, 0, totalPages - 1);

        // Remove old entries.
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Calculate which items belong on this page.
        int startIndex = currentPage * itemsPerPage;

        int endIndex = Mathf.Min(
            startIndex + itemsPerPage,
            allItems.Count
        );

        // Create the entries for this page.
        for (int i = startIndex; i < endIndex; i++)
        {
            ItemData item = allItems[i];

            if (item == null)
                continue;

            GameObject newEntry = Instantiate(
                itemEntryPrefab,
                contentParent
            );

            ItemInventoryEntry entry =
                newEntry.GetComponent<ItemInventoryEntry>();

            if (entry != null)
                entry.Setup(item, this);
        }

        // Update page information.
        if (pageText != null)
        {
            pageText.text =
                "Page " + (currentPage + 1) + " / " + totalPages;
        }

        if (previousPageButton != null)
            previousPageButton.interactable = currentPage > 0;

        if (nextPageButton != null)
            nextPageButton.interactable = currentPage < totalPages - 1;

        RefreshSelectedItem();
    }

    public void NextPage()
    {
        currentPage++;
        RefreshInventory();
    }

    public void PreviousPage()
    {
        currentPage--;
        RefreshInventory();
    }

    public void SelectItem(ItemData item)
    {
        selectedItem = item;
        RefreshSelectedItem();
    }

    public void RefreshSelectedItem()
    {
        if (selectedItem == null)
        {
            ClearSelectedItem();
            return;
        }

        bool unlocked =
            ItemCollectionManager.Instance != null &&
            ItemCollectionManager.Instance.IsItemUnlocked(selectedItem);

        if (selectedItemImage != null)
        {
            selectedItemImage.sprite = selectedItem.itemSprite;
            selectedItemImage.preserveAspect = true;
            selectedItemImage.enabled = selectedItem.itemSprite != null;
            selectedItemImage.color = unlocked
                ? Color.white
                : Color.black;
        }

        if (selectedItemName != null)
        {
            selectedItemName.text = unlocked
                ? selectedItem.itemName
                : "???";
        }

        if (selectedItemDescription != null)
        {
            selectedItemDescription.text = unlocked
                ? selectedItem.itemDescription
                : "Discover this item to unlock its description!";
        }

        if (selectedItemRarity != null)
            selectedItemRarity.text = unlocked
                ? selectedItem.rarity.ToString()
                : "???";

        if (selectedItemSlot != null)
            selectedItemSlot.text = selectedItem.equipmentSlot.ToString();

        bool equipped = unlocked &&
            ItemEquipmentManager.Instance != null &&
            ItemEquipmentManager.Instance.IsEquipped(selectedItem);

        if (selectedItemStatus != null)
        {
            selectedItemStatus.text = !unlocked
                ? "Not discovered"
                : equipped ? "Currently equipped" : "Not equipped";
        }

        if (equipButton != null)
            equipButton.interactable = unlocked && !equipped;

        if (unequipButton != null)
            unequipButton.interactable = equipped;
    }

    public void EquipSelectedItem()
    {
        if (selectedItem == null ||
            ItemEquipmentManager.Instance == null)
            return;

        ItemEquipmentManager.Instance.EquipItem(selectedItem);
    }

    public void UnequipSelectedItem()
    {
        if (selectedItem == null ||
            ItemEquipmentManager.Instance == null)
            return;

        ItemEquipmentManager.Instance.UnequipItem(
            selectedItem.equipmentSlot
        );
    }

    private void ClearSelectedItem()
    {
        if (selectedItemImage != null)
            selectedItemImage.enabled = false;

        if (selectedItemName != null)
            selectedItemName.text = "Select an item";

        if (selectedItemDescription != null)
            selectedItemDescription.text = "";

        if (selectedItemRarity != null)
            selectedItemRarity.text = "";

        if (selectedItemSlot != null)
            selectedItemSlot.text = "";

        if (selectedItemStatus != null)
            selectedItemStatus.text = "";

        if (equipButton != null)
            equipButton.interactable = false;

        if (unequipButton != null)
            unequipButton.interactable = false;
    }
}
