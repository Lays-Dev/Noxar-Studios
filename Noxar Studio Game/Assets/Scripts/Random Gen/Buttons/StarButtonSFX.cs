
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarButtonSFX : MonoBehaviour
{
    [Header("Button Sound")]
    public AudioSource audioSource;
    public AudioClip buttonSFX;

    private bool isPlaying = false;

    private void Awake()
    {
        // Keep the sound manager alive when changing scenes.
        DontDestroyOnLoad(gameObject);
    }

    // Use this for a button that changes scenes.
    public void PlaySoundThenLoadScene(string sceneName)
    {
        if (isPlaying) return;

        StartCoroutine(LoadSceneAfterSound(sceneName));
    }

    // Use this for a button that deactivates a panel or object.
    public void PlaySoundThenDeactivate(GameObject objectToDeactivate)
    {
        if (isPlaying) return;

        StartCoroutine(DeactivateAfterSound(objectToDeactivate));
    }

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        isPlaying = true;

        PlayButtonSound();

        // Wait for the entire sound to finish.
        yield return new WaitForSecondsRealtime(buttonSFX.length);

        SceneManager.LoadScene(sceneName);
        isPlaying = false;
    }

    private IEnumerator DeactivateAfterSound(GameObject objectToDeactivate)
    {
        isPlaying = true;

        PlayButtonSound();

        // Wait for the entire sound to finish.
        yield return new WaitForSecondsRealtime(buttonSFX.length);

        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        isPlaying = false;
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonSFX != null)
        {
            audioSource.PlayOneShot(buttonSFX);
        }
        else
        {
            Debug.LogWarning("UIButtonSound: AudioSource or SFX is missing!");
        }
    }
}

