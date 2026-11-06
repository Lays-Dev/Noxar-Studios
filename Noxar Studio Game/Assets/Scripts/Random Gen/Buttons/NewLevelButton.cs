using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelButton : MonoBehaviour
{
    [Header("Scene Settings")]
    public string counterSceneName = "Counter";

    public void StartNewLevel()
    {
        // Get the persistent LevelManager.
        LevelManager levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found!");
            return;
        }

        // Generate the next level.
        levelManager.StartNextLevel();

        // Load the Order Counter scene.
        SceneManager.LoadScene(counterSceneName);

        Debug.Log("New Level Started!");
    }
}