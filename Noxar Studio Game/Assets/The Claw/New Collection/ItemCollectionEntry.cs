
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollectionEntry : MonoBehaviour
{
    [Header("UI References")]
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemRarityText;
    public TMP_Text itemStatusText;

    [Header("Button")]
    public Button itemButton;

    [Header("Locked Appearance")]
    public Color lockedColor = Color.black;

    private ItemData currentItem;
    private ItemCollectionUI collectionUI;

    public void Setup(ItemData item, ItemCollectionUI menu)
    {
        if (item == null)
            return;

        currentItem = item;
        collectionUI = menu;

        UpdateAppearance();

        // Make this cosmetic clickable.
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(SelectItem);
        }
    }

    private void UpdateAppearance()
    {
        bool unlocked = currentItem.isUnlocked;

        if (itemImage != null)
        {
            itemImage.sprite = currentItem.itemSprite;
            itemImage.color = unlocked ? Color.white : lockedColor;
        }

        if (itemNameText != null)
        {
            itemNameText.text = unlocked
                ? currentItem.itemName
                : "???";
        }

        if (itemRarityText != null)
        {
            itemRarityText.text = unlocked
                ? currentItem.rarity.ToString()
                : "???";
        }

        if (itemStatusText != null)
        {
            itemStatusText.text = unlocked
                ? "Collected!"
                : "Not discovered";
        }
    }

    // Called when the player clicks this cosmetic.
    private void SelectItem()
    {
        if (collectionUI != null)
        {
            collectionUI.ShowSelectedItem(currentItem);
        }
    }
}
