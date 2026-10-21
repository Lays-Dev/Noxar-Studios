
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneTap : MonoBehaviour
{
    [Header("Patron Calling")]
    public bool patronCalling = false;

    [Header("Sound Effect")]
    public AudioSource audioSource;
    public AudioClip phoneTapSFX;
    public AudioClip phoneRingingSFX;

    [Header("Order")]
    public GameObject phoneOrder;

    [Header("Level 2 Phone Order")]
    public GameObject johnnyNPC;
    public GameObject johnnyOrder;
    public GameObject johnnyDish;
    public GameObject johnnyCurrency;

    private bool ringStopped = false;

    private void Start()
    {
        // Hide Johnny only if the phone order was already taken.
        if (TapSprite.phoneOrderTaken)
        {
            HideJohnny();
        }
    }

    private void Update()
    {
        // Play the looping ring while the patron is calling.
        if (patronCalling && !ringStopped)
        {
            if (audioSource != null &&
                (!audioSource.isPlaying ||
                 audioSource.clip != phoneRingingSFX))
            {
                audioSource.clip = phoneRingingSFX;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        // Check for mobile touch input.
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

        // Only continue if the phone was tapped.
        if (hit.collider == null ||
            hit.collider.gameObject != gameObject)
            return;

        Debug.Log("Phone tapped!");

        // Only answer if the phone is ringing.
        if (patronCalling)
        {
            // Stop the ringing sound.
            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }

            ringStopped = true;
            patronCalling = false;

            // Show the next phone order.
            if (phoneOrder != null)
                phoneOrder.SetActive(true);

 
                // Mark the phone order as taken.
                TapSprite.phoneOrderTaken = true;
                TapSprite.patronHasOrdered = true;
                // Reset and restart the timer for the phone order.
                if (GameTimer.Instance != null)
                {
                    GameTimer.Instance.ResetTimer();
                    Debug.Log("Phone order taken! Timer reset and restarted.");
                }

                // Reset the previous order's minigame progress.
                EndMinigame.gatherComplete = false;
                GrillingUIManager.grillingCompleted = false;

                // Reset the phone-specific completion flags.
                TapSprite.phoneOrderGatherComplete = false;
                TapSprite.phoneOrderGrillComplete = false;

                Debug.Log("Phone order taken. Gather and grill again!");


            // Hide Johnny's previous order objects.
            HideJohnny();

            Debug.Log("Phone order answered!");
        }

        // Play the phone tap sound.
        if (audioSource != null && phoneTapSFX != null)
        {
            audioSource.PlayOneShot(phoneTapSFX);
        }
    }

    private void HideJohnny()
    {
        if (johnnyNPC != null)
            johnnyNPC.SetActive(false);

        if (johnnyOrder != null)
            johnnyOrder.SetActive(false);

        if (johnnyDish != null)
            johnnyDish.SetActive(false);

        if (johnnyCurrency != null)
            johnnyCurrency.SetActive(false);

        Debug.Log("Johnny's previous order objects are hidden.");
    }
}
