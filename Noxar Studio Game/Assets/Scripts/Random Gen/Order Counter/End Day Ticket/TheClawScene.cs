using UnityEngine;
using UnityEngine.SceneManagement;

public class TheClawScene : MonoBehaviour
{
    public string clawSceneName = "The Claw";

    public void LoadClawScene()
    {
        SceneManager.LoadScene(clawSceneName);
    }
}
