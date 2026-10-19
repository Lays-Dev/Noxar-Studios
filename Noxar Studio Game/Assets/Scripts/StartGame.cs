using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Order Counter Blockout");
    }

    public void SceneLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

}
