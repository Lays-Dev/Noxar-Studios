
using UnityEngine;
using UnityEngine.UI;

public class PlayerAppearanceUI : MonoBehaviour
{
    [Header("Player Appearance")]
    public Image playerImage;
    public Animator playerAnimator;

    [Header("Equipment Slot")]
    public EquipmentSlot slotToDisplay = EquipmentSlot.Body;

    private void Start()
    {
        UpdatePlayerAppearance();
    }

    public void UpdatePlayerAppearance()
    {
        if (ItemCollectionManager.Instance == null)
        {
            Debug.LogWarning("ItemCollectionManager not found!");
            return;
        }

        var allItems =
            ItemCollectionManager.Instance.GetAllItems();

        foreach (ItemData item in allItems)
        {
            if (item == null)
                continue;

            if (item.equipmentSlot == slotToDisplay &&
                item.isEquipped)
            {
                // Change the outfit sprite.
                if (playerImage != null)
                {
                    playerImage.sprite = item.itemSprite;
                    playerImage.enabled = true;
                }

                // Change the outfit idle animation.
                if (playerAnimator != null)
                {
                    playerAnimator.runtimeAnimatorController =
                        item.idleAnimation;

                    playerAnimator.Play("Idle", 0, 0f);
                }

                Debug.Log("Equipped outfit and animation: " +
                    item.itemName);

                return;
            }
        }

        Debug.Log("No equipped item found for: " + slotToDisplay);
    }
}

