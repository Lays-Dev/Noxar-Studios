using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void GoToOrderCounter()
    {
        SceneManager.LoadScene("Order Counter Blockout");
    }

    public void GoToKitchen()
    {
        Debug.Log("Going to Kitchen scene");
        SceneManager.LoadScene("Kitchen Blockout");
    }
}
