
using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemCollectionManager : MonoBehaviour
{
    public static ItemCollectionManager Instance;

    [Header("All Possible Items")]
    public List<ItemData> allItems = new List<ItemData>();

    public event Action OnItemCollectionChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Reset all cosmetic unlocks when the game starts.
        foreach (ItemData item in allItems)
        {
            if (item != null)
            {
                item.isUnlocked = false;
            }
        }

        Debug.Log("All cosmetic unlocks have been reset!");
    }

    // Selects a random locked item matching the capsule rarity.
    public ItemData GetRandomReward(CapsuleRarity capsuleRarity)
    {
        List<ItemData> availableRewards = new List<ItemData>();

        foreach (ItemData item in allItems)
        {
            if (item == null)
                continue;

            if (item.rarity == capsuleRarity &&
                !item.isUnlocked)
            {
                availableRewards.Add(item);
            }
        }

        if (availableRewards.Count == 0)
        {
            Debug.Log("All " + capsuleRarity + " rewards collected!");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(
            0,
            availableRewards.Count
        );

        ItemData selectedItem = availableRewards[randomIndex];

        // Unlock this specific item!
        UnlockItem(selectedItem);

        return selectedItem;
    }

    // Unlocks an individual cosmetic.
    public void UnlockItem(ItemData item)
    {
        if (item == null)
            return;

        if (item.isUnlocked)
        {
            Debug.Log(item.itemName + " is already unlocked!");
            return;
        }

        item.isUnlocked = true;

        Debug.Log(item.itemName + " has been unlocked!");

        OnItemCollectionChanged?.Invoke();
    }

    // Check whether an item is unlocked.
    public bool IsItemUnlocked(ItemData item)
    {
        if (item == null)
            return false;

        return item.isUnlocked;
    }

    // Returns all unlocked items.
    public List<ItemData> GetUnlockedItems()
    {
        List<ItemData> unlocked = new List<ItemData>();

        foreach (ItemData item in allItems)
        {
            if (item != null && item.isUnlocked)
                unlocked.Add(item);
        }

        return unlocked;
    }

    // Returns every item, locked and unlocked.
    public List<ItemData> GetAllItems()
    {
        return allItems;
    }
}
