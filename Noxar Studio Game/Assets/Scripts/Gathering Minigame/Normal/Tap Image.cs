using UnityEngine;
using UnityEngine.InputSystem;

public class TapImage : MonoBehaviour
{
    public float speed = 1f;
    public GameObject nextIngredient;
    public GameObject objectToDestroy;

    private RectTransform image;
    private int tapCount = 0;

    private void Start()
    {
        image = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Rotate back and forth
        float rotation = Mathf.Lerp(
            -28f,
            45f,
            Mathf.PingPong(Time.time * speed, 1f)
        );

        image.localRotation = Quaternion.Euler(0f, 0f, rotation);

        // Mobile touch
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            Canvas canvas = GetComponentInParent<Canvas>();

            Camera cam = null;

            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                cam = canvas.worldCamera;
            }

            if (RectTransformUtility.RectangleContainsScreenPoint(
                image,
                touchPosition,
                cam))
            {
                tapCount++;

                Debug.Log("Image tapped: " + tapCount);

                if (tapCount >= 3)
                {
                    if (nextIngredient != null)
                    {
                        nextIngredient.SetActive(true);
                    }
                    else
                    {
                        Destroy(objectToDestroy);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}