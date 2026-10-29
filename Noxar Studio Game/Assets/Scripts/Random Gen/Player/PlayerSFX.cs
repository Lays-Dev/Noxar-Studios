using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSFX : MonoBehaviour, IPointerClickHandler
{
    [Header("Tap Sound")]
    public AudioSource audioSource;
    public AudioClip tapSFX;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && tapSFX != null)
        {
            audioSource.PlayOneShot(tapSFX);
        }
        else
        {
            Debug.LogWarning("UIImageTapSFX: AudioSource or Tap SFX is missing!", this);
        }
    }
}
