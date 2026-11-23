using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemCollectionManager : MonoBehaviour
{
    public static ItemCollectionManager Instance;

    [Header("All Possible Items")]
    public List<ItemData> allItems = new List<ItemData>();

    // Stores all items the player has unlocked.
    private List<ItemData> unlockedItems = new List<ItemData>();

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
    }

    public ItemData GetRandomReward(CapsuleRarity capsuleRarity)
    {
        List<ItemData> availableRewards = new List<ItemData>();

        foreach (ItemData item in allItems)
        {
            if (item == null)
                continue;

            if (item.rarity == capsuleRarity &&
                !unlockedItems.Contains(item))
            {
                availableRewards.Add(item);
            }
        }

        if (availableRewards.Count == 0)
        {
            Debug.Log("All " + capsuleRarity + " rewards collected!");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, availableRewards.Count);

        ItemData selectedItem = availableRewards[randomIndex];

        unlockedItems.Add(selectedItem);

        Debug.Log("New item unlocked: " + selectedItem.itemName);

        OnItemCollectionChanged?.Invoke();

        return selectedItem;
    }

    public bool IsItemUnlocked(ItemData item)
    {
        return unlockedItems.Contains(item);
    }

    // Returns all unlocked items.
    public List<ItemData> GetUnlockedItems()
    {
        return new List<ItemData>(unlockedItems);
    }

    // Returns every possible item, including locked items.
    public List<ItemData> GetAllItems()
    {
        return allItems;
    }
}
