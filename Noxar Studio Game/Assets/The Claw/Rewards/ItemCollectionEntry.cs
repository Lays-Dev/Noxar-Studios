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

    public void Setup(ItemData item, bool isUnlocked)
    {
        if (item == null)
            return;

        if (itemImage != null)
        {
            itemImage.sprite = item.itemSprite;
            itemImage.enabled = item.itemSprite != null;

            // Darken the image when the item is locked.
            itemImage.color = isUnlocked
                ? Color.white
                : new Color(0.15f, 0.15f, 0.15f, 1f);
        }

        if (itemNameText != null)
        {
            itemNameText.text = isUnlocked
                ? item.itemName
                : "???";
        }

        if (itemRarityText != null)
        {
            itemRarityText.text = isUnlocked
                ? item.rarity.ToString()
                : "???";
        }

        if (itemStatusText != null)
        {
            itemStatusText.text = isUnlocked
                ? "Collected!"
                : "Not discovered";
        }
    }
}
