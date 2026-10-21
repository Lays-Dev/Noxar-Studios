using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playerClip;

    private void Update()
    {
        // Check if the player clicks or taps.
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition =
                Camera.main.ScreenToWorldPoint(touchPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            // Check whether this sprite was clicked.
            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                PlayClickSound();
            }
        }
    }

    private void PlayClickSound()
    {
        if (audioSource != null && playerClip != null)
        {
            audioSource.PlayOneShot(playerClip);
        }
    }

}
