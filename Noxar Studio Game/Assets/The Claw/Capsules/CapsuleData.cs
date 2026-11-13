using UnityEngine;

public enum CapsuleRarity
{
    Common,
    Uncommon,
    Rare,
    Cow,
}

[CreateAssetMenu(
    fileName = "New Capsule",
    menuName = "Claw Machine/Capsule"
)]
public class CapsuleData : ScriptableObject
{
    [Header("Capsule Information")]
    public string capsuleName;
    public Sprite capsuleSprite;

    [Header("Rarity")]
    public CapsuleRarity rarity;

    [Tooltip("Higher weight means more likely to spawn.")]
    [Min(0)]
    public float spawnWeight = 50f;
}
