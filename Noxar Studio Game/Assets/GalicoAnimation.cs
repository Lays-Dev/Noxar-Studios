using UnityEngine;
using UnityEngine.InputSystem;

public class GalicoAnimation : MonoBehaviour
{
    public Animator animator;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Touchscreen.current == null)
            return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition =
                mainCamera.ScreenToWorldPoint(touchPosition);

            Vector2 touchPoint = new Vector2(
                worldPosition.x, worldPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(
                touchPoint, Vector2.zero);

            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                animator.SetTrigger("Tap");
            }
        }
    }
}
