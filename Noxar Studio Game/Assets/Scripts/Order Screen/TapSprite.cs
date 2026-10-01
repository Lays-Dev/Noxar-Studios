using UnityEngine;
using UnityEngine.InputSystem;

public class TapSprite : MonoBehaviour
{
    public GameObject image;
    public GameObject dish;
    

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
                
                if (GatheringUI.gatherComplete)
                {
                    dish.SetActive(true);
                }
                else
                {
                    image.SetActive(true);
                }
            }
        }
    }
}
