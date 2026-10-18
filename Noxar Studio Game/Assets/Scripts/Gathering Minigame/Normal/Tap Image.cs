
using UnityEngine;
using UnityEngine.InputSystem;

public class TapImage : MonoBehaviour
{
    public GameObject nextIngredient;
    public GameObject armParent; // Reference to the parent GameObject of the arm

    private RectTransform image;
    private int tapCount = 0;
    public bool lastIngredient = false;
    public GameObject fridgeOpen;
    public AudioSource tapSFX; // Reference to the AudioSource component for tap sound effect


    private void Start()
    {
        image = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Check for mobile touch.
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            Canvas canvas = GetComponentInParent<Canvas>();
            Camera cam = null;

            if (canvas != null &&
                canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                cam = canvas.worldCamera;
            }

            // Check whether the ingredient was tapped.
            if (RectTransformUtility.RectangleContainsScreenPoint(
                image, touchPosition, cam))
            {
                tapCount++;

                Debug.Log("Ingredient taps: " + tapCount);
                if (tapSFX != null)
                    {
                        tapSFX.Play();
                    }

                // Require two taps before spawning the next ingredient.
                if (tapCount >= 2)
                {

                    if (lastIngredient)
                    {
                        Debug.Log("Last ingredient tapped twice.");
                        fridgeOpen.SetActive(false);
                    }
               
                    if (nextIngredient != null)
                    {
                        nextIngredient.SetActive(true);

                    }

                        armParent.SetActive(false);
                }
            }
        }
    }
}
