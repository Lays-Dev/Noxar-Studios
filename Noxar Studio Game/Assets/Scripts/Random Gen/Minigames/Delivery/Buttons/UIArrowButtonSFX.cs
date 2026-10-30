
using UnityEngine;
using UnityEngine.UI;

public class UIArrowButtonSFX : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip buttonClickSFX;

    [Header("Button")]
    public Button arrowButton;

    [Header("Spam Prevention")]
    public float soundCooldown = 0.2f;

    private float nextSoundTime = 0f;

    private void Start()
    {
        if (arrowButton == null)
        {
            arrowButton = GetComponent<Button>();
        }

        arrowButton.onClick.AddListener(PlayClickSFX);
    }

    private void PlayClickSFX()
    {
        // Ignore clicks during cooldown.
        if (Time.time < nextSoundTime)
            return;

        if (audioSource != null && buttonClickSFX != null)
        {
            audioSource.PlayOneShot(buttonClickSFX);

            // Set the next allowed sound time.
            nextSoundTime = Time.time + soundCooldown;
        }
    }

    private void OnDestroy()
    {
        if (arrowButton != null)
        {
            arrowButton.onClick.RemoveListener(PlayClickSFX);
        }
    }
}
