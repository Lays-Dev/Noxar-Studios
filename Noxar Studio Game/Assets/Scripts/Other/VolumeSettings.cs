using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        float vol = PlayerPrefs.GetFloat("volume", 1f);
        slider.value = vol;
        AudioListener.volume = vol;

        slider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float vol)
    {
        AudioListener.volume = vol;
        PlayerPrefs.SetFloat("volume", vol);
    }
}