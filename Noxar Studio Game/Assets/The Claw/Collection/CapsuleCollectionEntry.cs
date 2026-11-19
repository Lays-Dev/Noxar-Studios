using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CapsuleCollectionEntry : MonoBehaviour
{
    [Header("UI References")]
    public Image capsuleImage;
    public TMP_Text capsuleNameText;
    public TMP_Text capsuleCountText;

    public void Setup(CapsuleData capsuleData, int collectedAmount)
    {
        if (capsuleData == null)
            return;

        // Check whether the player has collected this capsule.
        bool isUnlocked = collectedAmount > 0;

        if (capsuleImage != null)
        {
            capsuleImage.sprite = capsuleData.capsuleSprite;
            capsuleImage.enabled = capsuleData.capsuleSprite != null;

            // Darken the image if the capsule is locked.
            capsuleImage.color = isUnlocked
                ? Color.white
                : Color.black;
        }

        // Display the name or question marks.
        if (capsuleNameText != null)
        {
            capsuleNameText.text = isUnlocked
                ? capsuleData.capsuleName
                : "???";
        }

        // Display the amount collected.
        if (capsuleCountText != null)
        {
            capsuleCountText.text = isUnlocked
                ? "Collected: " + collectedAmount
                : "Not discovered";
        }
    }
}
