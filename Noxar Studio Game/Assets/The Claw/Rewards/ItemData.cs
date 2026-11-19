using UnityEngine;

[CreateAssetMenu(
    fileName = "New Item",
    menuName = "Claw Machine/Item"
)]
public class ItemData : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    public Sprite itemSprite;

    [TextArea]
    public string itemDescription;

    [Header("Item Rarity")]
    public CapsuleRarity rarity;
}

