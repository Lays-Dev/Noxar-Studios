
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaptureRewardUI : MonoBehaviour
{
    [Header("Reward Display")]
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemRarityText;
    public TMP_Text messageText;
    public TMP_Text itemDescriptionText;

    [Header("Reward Objects")]
    public GameObject rewardContent;
    public GameObject noRewardsMessage;

    public void ShowReward(ItemData item)
    {
        // No available rewards.
        if (item == null)
        {
            ShowNoRewards();
            return;
        }

        if (rewardContent != null)
            rewardContent.SetActive(true);

        if (noRewardsMessage != null)
            noRewardsMessage.SetActive(false);

        // Display item sprite.
        if (itemImage != null)
        {
            itemImage.sprite = item.itemSprite;
            itemImage.enabled = item.itemSprite != null;
            itemImage.preserveAspect = true;
        }

        // Display item name.
        if (itemNameText != null)
            itemNameText.text = item.itemName;

        // Display item rarity.
        if (itemRarityText != null)
            itemRarityText.text = item.rarity.ToString();

        // Display capture message.
        if (messageText != null)
            messageText.text = "Capsule Captured!";

        // Display item description.
        if (itemDescriptionText != null)
            itemDescriptionText.text = item.itemDescription;

        Debug.Log("REWARD RECEIVED: " + item.itemName);
        Debug.Log("DESCRIPTION: " + item.itemDescription);
    }

    private void ShowNoRewards()
    {
        if (rewardContent != null)
            rewardContent.SetActive(false);

        if (noRewardsMessage != null)
            noRewardsMessage.SetActive(true);

        if (messageText != null)
            messageText.text = "All rewards of this rarity have been collected!";

        if (itemDescriptionText != null)
            itemDescriptionText.text = "";
    }
}
