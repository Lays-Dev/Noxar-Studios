
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

    [Header("Locked Appearance")]
    public Color lockedColor = Color.black;

    // Updates this cosmetic's appearance.
    public void Setup(ItemData item)
    {
        if (item == null)
            return;

        bool unlocked = item.isUnlocked;

        // Display the cosmetic image.
        if (itemImage != null)
        {
            itemImage.sprite = item.itemSprite;

            // Darken the image if locked.
            itemImage.color = unlocked ? Color.white : lockedColor;
        }

        // Display the cosmetic name.
        if (itemNameText != null)
        {
            itemNameText.text = unlocked ? item.itemName : "???";
        }

        // Display the cosmetic rarity.
        if (itemRarityText != null)
        {
            itemRarityText.text = unlocked
                ? item.rarity.ToString()
                : "???";
        }

        // Display unlock status.
        if (itemStatusText != null)
        {
            itemStatusText.text = unlocked
                ? "Collected!"
                : "Not discovered";
        }
    }
}

