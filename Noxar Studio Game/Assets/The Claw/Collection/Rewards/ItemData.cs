using UnityEngine;

public enum EquipmentSlot
{
    Head,
    Body,
    Accessories
}

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

    [Header("Equipment")]
    public EquipmentSlot equipmentSlot;
}

