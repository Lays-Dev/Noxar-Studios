using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButton : MonoBehaviour
{
   public void MainMenuButtonClick()
{
    SceneManager.LoadScene("MainMenu");
}
}
