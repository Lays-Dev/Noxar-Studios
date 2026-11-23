
using UnityEngine;
using UnityEngine.UI;

public class CharacterPreviewUI : MonoBehaviour
{
    [Header("Character Preview")]
    public Image characterBase;

    [Header("Equipment Images")]
    public Image headImage;
    public Image bodyImage;
    public Image accessoriesImage;

    private void OnEnable()
    {
        SubscribeToEquipment();
        RefreshPreview();
    }

    private void Start()
    {
        SubscribeToEquipment();
        RefreshPreview();
    }

    private void OnDisable()
    {
        if (ItemEquipmentManager.Instance != null)
        {
            ItemEquipmentManager.Instance.OnEquipmentChanged
                -= RefreshPreview;
        }
    }

    private void SubscribeToEquipment()
    {
        if (ItemEquipmentManager.Instance != null)
        {
            ItemEquipmentManager.Instance.OnEquipmentChanged
                -= RefreshPreview;

            ItemEquipmentManager.Instance.OnEquipmentChanged
                += RefreshPreview;
        }
    }

    public void RefreshPreview()
    {
        if (ItemEquipmentManager.Instance == null)
            return;

        UpdateEquipmentImage(
            headImage,
            EquipmentSlot.Head
        );

        UpdateEquipmentImage(
            bodyImage,
            EquipmentSlot.Body
        );

        UpdateEquipmentImage(
            accessoriesImage,
            EquipmentSlot.Accessories
        );
    }

    private void UpdateEquipmentImage(
        Image targetImage,
        EquipmentSlot slot)
    {
        if (targetImage == null)
            return;

        ItemData equippedItem =
            ItemEquipmentManager.Instance.GetEquippedItem(slot);

        if (equippedItem != null)
        {
            targetImage.sprite = equippedItem.itemSprite;
            targetImage.enabled = equippedItem.itemSprite != null;
            targetImage.preserveAspect = true;
        }
        else
        {
            targetImage.sprite = null;
            targetImage.enabled = false;
        }
    }
}
