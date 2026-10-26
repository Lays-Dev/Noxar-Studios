/*using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public AudioSource soundEffect;

    public void GoToOrderCounter()
    {
        string sceneName = "Order Counter Blockout";

        if (LevelManager.Instance != null &&
            LevelManager.level2Unlocked == true)
        {
            sceneName = "Level2";
        }

        StartCoroutine(PlaySoundAndChangeScene(sceneName));
    }

    public void GoToKitchen()
    {
        StartCoroutine(
            PlaySoundAndChangeScene("Kitchen Blockout")
        );
    }

    IEnumerator PlaySoundAndChangeScene(string sceneName)
    {
        if (soundEffect != null)
        {
            soundEffect.Play();

            yield return new WaitWhile(
                () => soundEffect != null && soundEffect.isPlaying
            );
        }

        SceneManager.LoadScene(sceneName);
    }
}*/