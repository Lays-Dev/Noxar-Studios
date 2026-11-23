
using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemEquipmentManager : MonoBehaviour
{
    public static ItemEquipmentManager Instance;

    // Stores the currently equipped item for each slot.
    private Dictionary<EquipmentSlot, ItemData> equippedItems =
        new Dictionary<EquipmentSlot, ItemData>();

    public event Action OnEquipmentChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Equip an item into its assigned slot.
    public void EquipItem(ItemData item)
    {
        if (item == null)
            return;

        // The player must own the item first.
        if (ItemCollectionManager.Instance == null ||
            !ItemCollectionManager.Instance.IsItemUnlocked(item))
        {
            Debug.LogWarning("You have not unlocked this item!");
            return;
        }

        // Replace whatever is currently in this slot.
        equippedItems[item.equipmentSlot] = item;

        Debug.Log(
            item.itemName + " equipped in " + item.equipmentSlot
        );

        OnEquipmentChanged?.Invoke();
    }

    // Remove the equipped item from a slot.
    public void UnequipItem(EquipmentSlot slot)
    {
        if (equippedItems.ContainsKey(slot))
        {
            equippedItems.Remove(slot);

            Debug.Log("Unequipped: " + slot);

            OnEquipmentChanged?.Invoke();
        }
    }

    // Get the item currently equipped in a slot.
    public ItemData GetEquippedItem(EquipmentSlot slot)
    {
        if (equippedItems.ContainsKey(slot))
            return equippedItems[slot];

        return null;
    }

    // Check if an item is currently equipped.
    public bool IsEquipped(ItemData item)
    {
        if (item == null)
            return false;

        return GetEquippedItem(item.equipmentSlot) == item;
    }
}
