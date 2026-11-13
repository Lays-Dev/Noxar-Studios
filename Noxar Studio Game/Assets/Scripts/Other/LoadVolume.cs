using UnityEngine;

public class LoadVolume : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1f);
    }
}