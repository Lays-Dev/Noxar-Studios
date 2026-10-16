
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TapSprite : MonoBehaviour
{
    public GameObject image;
    public GameObject dish;
    public GameObject currency;
    public Button EndDay;

    // Tracks whether the patron's order has been taken.
    public static bool patronHasOrdered = false;

    // Tracks whether Patron 2's order has been taken.
    public static bool level2OrderTaken = false;

    private void Update()
    {
        if (Touchscreen.current == null)
            return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            Vector2 worldPosition =
                Camera.main.ScreenToWorldPoint(touchPosition);

            RaycastHit2D hit =
                Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                Debug.Log("Patron was tapped!");

                // LEVEL 2: PATRON 2
                if (LevelManager.level2Unlocked)
                {
                    // First tap: take the order.
                    if (!level2OrderTaken)
                    {
                        level2OrderTaken = true;
                        patronHasOrdered = true;

                        if (image != null)
                            image.SetActive(true);

                        Debug.Log("Patron 2 order taken!");
                    }
                    // Later tap: serve only after both minigames are won.
                    else if (EndMinigame.gatherComplete &&
                             GrillingUIManager.grillingCompleted)
                    {
                        ServeDish();
                    }
                    else
                    {
                        Debug.Log(
                            "Complete gathering and win grilling before serving."
                        );
                    }
                }
                // LEVEL 1: PATRON 1
                else
                {
                    if (!patronHasOrdered)
                    {
                        patronHasOrdered = true;

                        if (image != null)
                            image.SetActive(true);

                        Debug.Log("Patron 1 order taken!");
                    }
                    else if (EndMinigame.gatherComplete)
                    {
                        ServeDish();
                    }
                    else
                    {
                        Debug.Log("Complete gathering before serving.");
                    }
                }
            }
        }
    }

    private void ServeDish()
    {
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
        }

        if (dish != null)
            dish.SetActive(true);

        if (currency != null)
            currency.SetActive(true);

        patronHasOrdered = false;

        if (EndDay != null)
            EndDay.gameObject.SetActive(true);

        Debug.Log("Dish served!");
    }
}
