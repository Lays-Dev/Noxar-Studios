
using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemCollectionManager : MonoBehaviour
{
    public static ItemCollectionManager Instance;

    [Header("All Possible Items")]
    public List<ItemData> allItems = new List<ItemData>();

    // Items the player has already unlocked.
    private List<ItemData> unlockedItems = new List<ItemData>();

    // Notifies the item collection screen when an item is unlocked.
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

    // Selects a random item matching the capsule's rarity.
    public ItemData GetRandomReward(CapsuleRarity capsuleRarity)
    {
        List<ItemData> availableRewards = new List<ItemData>();

        // Find items with matching rarity that are not unlocked.
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

        // No rewards remain for this rarity.
        if (availableRewards.Count == 0)
        {
            Debug.Log("All " + capsuleRarity + " rewards collected!");
            return null;
        }

        // Pick one random available item.
        int randomIndex = UnityEngine.Random.Range(0, availableRewards.Count);

        ItemData selectedItem = availableRewards[randomIndex];

        // Permanently unlock it for this game session.
        unlockedItems.Add(selectedItem);

        Debug.Log("New item unlocked: " + selectedItem.itemName);

        // Update the item collection screen.
        OnItemCollectionChanged?.Invoke();

        return selectedItem;
    }

    // Check whether an item has been unlocked.
    public bool IsItemUnlocked(ItemData item)
    {
        return unlockedItems.Contains(item);
    }
}
