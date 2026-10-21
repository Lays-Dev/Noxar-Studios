
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TapSprite : MonoBehaviour
{
    [Header("Patron")]
    public GameObject image;
    public GameObject dish;
    public GameObject currency;
    public Button EndDay;

    [Header("Phone Call")]
    public PhoneTap phoneTap;

    [Header("Audio")]
    public AudioSource galicoTalkSFX;

    [Header("Order Settings")]
    public bool isFinalOrder = false;

    // Tracks whether the current order has been taken.
    public static bool patronHasOrdered = false;

    // Tracks whether Patron 2's order has been taken.
    public static bool level2OrderTaken = false;

    public static float currencyAmount = 0f;
    public static bool phoneOrderTaken = false;
    // Remembers that the Level 2 phone order was completed.
    public static bool level2PhoneOrderCompleted = false;
    // Tracks gathering and grilling specifically for the phone order.
    public static bool phoneOrderGatherComplete = false;
    public static bool phoneOrderGrillComplete = false;

    private void Start()
    {
        // Hide End Day until the final order is served.
        if (EndDay != null)
        {
            EndDay.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Mobile touch input.
        if (Touchscreen.current == null)
            return;

        if (!Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            return;

        Vector2 touchPosition =
            Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition =
            Camera.main.ScreenToWorldPoint(touchPosition);

        RaycastHit2D hit = Physics2D.Raycast(
            worldPosition,
            Vector2.zero
        );

        // Check if this patron was tapped.
        if (hit.collider == null ||
            hit.collider.gameObject != gameObject)
            return;

        // Play patron dialogue SFX.
        if (galicoTalkSFX != null)
        {
            galicoTalkSFX.Play();
        }

        Debug.Log("Patron was tapped!");

        // LEVEL 2
        if (LevelManager.level2Unlocked)
        {
            // First order: take Patron 2's order.
            if (!level2OrderTaken)
            {
                TakeOrder();

                Debug.Log("Patron 2 order taken!");
            }
            // Serve only after both minigames are completed.
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
        // LEVEL 1
        else
        {
            if (!patronHasOrdered)
            {
                TakeOrder();
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

    private void TakeOrder()
    {
        patronHasOrdered = true;

        if (LevelManager.level2Unlocked)
        {
            level2OrderTaken = true;
        }

        if (image != null)
        {
            image.SetActive(true);
        }
    }

    private void ServeDish()
    {
        // Pause the timer while serving.
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.PauseTimer();
        }

        // Show the completed order.
        if (dish != null)
        {
            dish.SetActive(true);
        }

        if (currency != null)
        {
            currency.SetActive(true);
        }

        currencyAmount += 10f;
        Debug.Log("Currency amount: " + currencyAmount);

        // The current order is now complete.
        patronHasOrdered = false;
        // Remember that the Level 2 phone order was completed.
        if (LevelManager.level2Unlocked &&
            level2OrderTaken &&
            TapSprite.phoneOrderTaken &&
            EndMinigame.gatherComplete &&
            GrillingUIManager.grillingCompleted)
        {
            level2PhoneOrderCompleted = true;

            Debug.Log("Level 2 phone order completed successfully!");
        }

        Debug.Log("Dish served!");

        // FINAL ORDER: show End Day.
        if (isFinalOrder)
        {
            if (EndDay != null)
            {
                EndDay.gameObject.SetActive(true);
            }

            return;
        }

        // MORE ORDERS REMAIN: ring the phone.
        if (phoneTap != null)
        {
            TapSprite.phoneOrderTaken = false;
            phoneTap.patronCalling = true;
        }
        else
        {
            Debug.LogWarning("PhoneTap reference is missing!");
        }
    }
}
