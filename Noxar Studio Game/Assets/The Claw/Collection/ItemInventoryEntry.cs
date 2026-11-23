
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInventoryEntry : MonoBehaviour
{
    [Header("UI References")]
    public Button itemButton;
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemRarityText;

    private ItemData currentItem;
    private ItemInventoryUI inventoryUI;

    public void Setup(ItemData item, ItemInventoryUI owner)
    {
        currentItem = item;
        inventoryUI = owner;

        bool unlocked =
            ItemCollectionManager.Instance.IsItemUnlocked(item);

        if (itemImage != null)
        {
            itemImage.sprite = item.itemSprite;
            itemImage.preserveAspect = true;

            itemImage.color = unlocked
                ? Color.white
                : new Color(0.2f, 0.2f, 0.2f, 1f);
        }

        if (itemNameText != null)
        {
            itemNameText.text = unlocked
                ? item.itemName
                : "???";
        }

        if (itemRarityText != null)
        {
            itemRarityText.text = unlocked
                ? item.rarity.ToString()
                : "Locked";
        }

        if (itemButton != null)
        {
            itemButton.onClick.RemoveAllListeners();
            itemButton.onClick.AddListener(SelectItem);
        }
    }

    private void SelectItem()
    {
        if (inventoryUI != null)
            inventoryUI.SelectItem(currentItem);
    }
}
