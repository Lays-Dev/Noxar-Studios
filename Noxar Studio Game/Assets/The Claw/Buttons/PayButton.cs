
using UnityEngine;
using UnityEngine.InputSystem;

public class PayButton : MonoBehaviour
{
    [Header("Warning Popup")]
    public GameObject warningPopup;

    private Camera mainCamera;
    private Collider2D buttonCollider;

    private void Awake()
    {
        mainCamera = Camera.main;
        buttonCollider = GetComponent<Collider2D>();

        if (warningPopup != null)
            warningPopup.SetActive(false);
    }

    private void Update()
    {
        CheckButtonTap();
    }

    private void CheckButtonTap()
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

        Vector2 worldPosition =
            mainCamera.ScreenToWorldPoint(screenPosition);

        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null &&
            hit.GetComponentInParent<PayButton>() == this)
        {
            // Open the purchase confirmation popup.
            if (warningPopup != null)
                warningPopup.SetActive(true);

            Debug.Log("Purchase confirmation opened!");
        }
    }

    public void DeactivateButton()
    {
        if (buttonCollider != null)
            buttonCollider.enabled = false;
    }

    public void EnableButton()
    {
        if (buttonCollider != null)
            buttonCollider.enabled = true;
    }
}
