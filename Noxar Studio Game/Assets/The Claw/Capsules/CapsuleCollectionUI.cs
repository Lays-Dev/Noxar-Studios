using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CapsuleCollectionUI : MonoBehaviour
{
    [Header("Collection UI")]
    public Image capsuleImage;
    public TMP_Text capsuleNameText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Display the capsule that was collected.
    public void ShowCapsule(CapsuleData capsuleData)
    {
        if (capsuleData == null)
        {
            Debug.LogWarning("No CapsuleData was provided!");
            return;
        }

        // Activate the Canvas.
        gameObject.SetActive(true);

        // Display capsule sprite.
        if (capsuleImage != null)
        {
            capsuleImage.sprite = capsuleData.capsuleSprite;
            capsuleImage.enabled = capsuleData.capsuleSprite != null;
        }

        // Display capsule name.
        if (capsuleNameText != null)
        {
            capsuleNameText.text = capsuleData.capsuleName;
        }

        Debug.Log("Collected: " + capsuleData.capsuleName);
    }

    // Optional: Close the collection Canvas.
    public void CloseCollectionUI()
    {
        gameObject.SetActive(false);
    }
}
