using UnityEngine;

public class KitchenEquippedReward : MonoBehaviour
{
    [Header("Reward Settings")]
    public ItemData cowPlushItem;

    [Header("Kitchen Object")]
    public GameObject cowPlushObject;

    private void Start()
    {
        Debug.Log("===== KITCHEN EQUIPMENT CHECK STARTED =====");

        if (ItemEquipmentManager.Instance == null)
        {
            Debug.LogError("ERROR: ItemEquipmentManager.Instance is NULL!");
            return;
        }

        if (cowPlushItem == null)
        {
            Debug.LogError("ERROR: Cow Plush ItemData is NOT assigned!");
            return;
        }

        if (cowPlushObject == null)
        {
            Debug.LogError("ERROR: Cow Plush GameObject is NOT assigned!");
            return;
        }

        // Print which item is currently stored in the Cow Plush slot.
        ItemData equippedItem =
            ItemEquipmentManager.Instance.GetEquippedItem(cowPlushItem.equipmentSlot);

        Debug.Log("Kitchen Item Name: " + cowPlushItem.itemName);
        Debug.Log("Kitchen Item Slot: " + cowPlushItem.equipmentSlot);

        if (equippedItem == null)
        {
            Debug.LogWarning("NOTHING IS EQUIPPED IN THIS SLOT!");
        }
        else
        {
            Debug.Log("Currently Equipped Item: " + equippedItem.itemName);
            Debug.Log("Equipped Item Asset: " + equippedItem.name);
            Debug.Log("Kitchen Item Asset: " + cowPlushItem.name);
            Debug.Log("Same Asset Reference: " + (equippedItem == cowPlushItem));
        }

        // Compare the actual equipped item to the Cow Plush.
        bool isEquipped =
            ItemEquipmentManager.Instance.IsEquipped(cowPlushItem);

        Debug.Log("FINAL RESULT - Cow Plush Equipped: " + isEquipped);

        cowPlushObject.SetActive(isEquipped);

        Debug.Log("Cow Plush Object Active: " + cowPlushObject.activeSelf);

        Debug.Log("===== KITCHEN EQUIPMENT CHECK FINISHED =====");
    }
}