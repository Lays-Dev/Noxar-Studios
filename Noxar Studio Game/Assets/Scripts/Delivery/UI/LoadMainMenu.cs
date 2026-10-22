using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}