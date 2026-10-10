using UnityEngine;

public class ButtonClick : MonoBehaviour
{

    public AudioSource buttonClickSound;

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
    }
}
