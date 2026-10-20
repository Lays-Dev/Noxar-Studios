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

    // Temporary bool
    private bool ringStopped = false;

    private void Update()
    {

        // Play the looping ring while the patron is calling
        if (patronCalling && !ringStopped)
        {
            if (!audioSource.isPlaying ||
                audioSource.clip != phoneRingingSFX)
            {
                audioSource.clip = phoneRingingSFX;
                audioSource.loop = true;
                audioSource.Play();
            }
        }


        // Check if a touchscreen is available
        if (Touchscreen.current == null)
            return;

        // Detect a new tap
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            // Get the tap position on the screen
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            // Convert the screen position to world position
            Vector3 worldPosition =
                Camera.main.ScreenToWorldPoint(touchPosition);

            // Check if the tap hits the phone
            RaycastHit2D hit = Physics2D.Raycast(
                worldPosition,
                Vector2.zero
            );

            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                Debug.Log("Phone tapped!");
                 // Stop the ringing sound
                if (patronCalling)
                {
                    audioSource.Stop();
                    audioSource.loop = false;
                    ringStopped = true;

                    phoneOrder.SetActive(true);
                    patronCalling = false;

                    TapSprite.patronHasOrdered = true;
                }

                // Play the normal phone tap sound
                audioSource.clip = phoneTapSFX;
                audioSource.Play();
            }
        }

        // Reset so the ring can play on the next call
        if (!patronCalling)
        {
            ringStopped = false;
        }
    }
}
