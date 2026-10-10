using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public AudioSource soundEffect;

    public void GoToOrderCounter()
    {
        StartCoroutine(PlaySoundAndChangeScene("Order Counter Blockout"));
    }

    public void GoToKitchen()
    {
        StartCoroutine(PlaySoundAndChangeScene("Kitchen Blockout"));
    }

    IEnumerator PlaySoundAndChangeScene(string sceneName)
    {
        soundEffect.Play();

        yield return new WaitWhile(() => soundEffect.isPlaying);

        SceneManager.LoadScene(sceneName);
    }
}
