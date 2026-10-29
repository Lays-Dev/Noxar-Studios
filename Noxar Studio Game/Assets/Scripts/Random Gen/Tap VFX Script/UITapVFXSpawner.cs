
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITapVFXSpawner : MonoBehaviour
{
    [Header("UI VFX")]
    public GameObject vfxPrefab;
    public Canvas targetCanvas;

    [Header("Animation")]
    public float effectLifetime = 1f;

    private RectTransform canvasRect;
    private Camera uiCamera;

    private void Awake()
    {
        if (targetCanvas == null)
            targetCanvas = GetComponentInParent<Canvas>();

        if (targetCanvas != null)
        {
            canvasRect = targetCanvas.GetComponent<RectTransform>();

            if (targetCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
                uiCamera = targetCanvas.worldCamera;
        }
    }

    private void Update()
    {
        Vector2 screenPosition;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        // Do not spawn an effect when tapping UI.
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        SpawnVFX(screenPosition);
    }

    private void SpawnVFX(Vector2 screenPosition)
    {
        if (vfxPrefab == null || targetCanvas == null)
            return;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPosition,
            uiCamera,
            out Vector2 localPoint))
        {
            return;
        }

        GameObject effect = Instantiate(
            vfxPrefab,
            targetCanvas.transform
        );

        RectTransform effectRect =
            effect.GetComponent<RectTransform>();

        if (effectRect != null)
        {
            effectRect.anchoredPosition = localPoint;
            effectRect.localRotation = Quaternion.identity;
            effectRect.localScale = Vector3.one;
        }

        Destroy(effect, effectLifetime);
    }
}
