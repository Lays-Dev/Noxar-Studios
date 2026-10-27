
using UnityEngine;
using UnityEngine.UI;

public class MinigameOrderItemUI : MonoBehaviour
{
    [Header("Minigame Icon")]
    public Image iconImage;

    public void Setup(MinigameData minigame)
    {
        if (minigame == null)
            return;

        if (iconImage != null)
        {
            // Assign the minigame icon sprite.
            iconImage.sprite = minigame.minigameIcon;

            // Enable the image only if the sprite is not null.
            iconImage.enabled = minigame.minigameIcon != null;
            iconImage.preserveAspect = true;
        }
    }
}
