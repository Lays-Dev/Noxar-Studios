
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer spriteRenderer;

    private CapsuleData capsuleData;

    private void Awake()
    {
        // Automatically find the SpriteRenderer.
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void Setup(CapsuleData data)
    {
        capsuleData = data;

        if (data == null)
        {
            Debug.LogError("CapsuleData is missing!", this);
            return;
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing!", this);
            return;
        }

        if (data.capsuleSprite == null)
        {
            Debug.LogError(
                "No capsule sprite assigned to " + data.capsuleName,
                this
            );
            return;
        }

        // Change the capsule's sprite.
        spriteRenderer.sprite = data.capsuleSprite;

        Debug.Log("Changed capsule sprite to: " + data.capsuleName);
    }

    public CapsuleData GetCapsuleData()
    {
        return capsuleData;
    }
}

