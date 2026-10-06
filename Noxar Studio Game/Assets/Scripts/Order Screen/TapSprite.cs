using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TapSprite : MonoBehaviour
{
    public GameObject image;
    public GameObject dish;
    public GameObject currency;
    public Button EndDay;

    // Checks if patron has ordered
    public static bool patronHasOrdered = false;

    

    private void Update()
    {
        if (Touchscreen.current == null)
            return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Sprite was tapped!");
                
                if (EndMinigame.gatherComplete)
                {
                    dish.SetActive(true);
                    currency.SetActive(true);
                    patronHasOrdered = false;
                    EndDay.gameObject.SetActive(true);

                }
                else
                {
                    patronHasOrdered = true;
                    image.SetActive(true);
                }
            }
        }
    }
}
