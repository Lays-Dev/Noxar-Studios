
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClawStartNewLevelButton : MonoBehaviour
{
    [Header("Button")]
    public Button startNewLevelButton;

    [Header("Scene Settings")]
    public string counterSceneName = "Counter";

    private void Start()
    {
        // Connect the button to StartNewLevel.
        if (startNewLevelButton != null)
        {
            startNewLevelButton.onClick.RemoveListener(StartNewLevel);
            startNewLevelButton.onClick.AddListener(StartNewLevel);
        }
    }

    public void StartNewLevel()
    {
        // Find the existing LevelManager.
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager not found!");
            return;
        }

        // Generate the next cooking level.
        LevelManager.Instance.StartNextLevel();

        // Return to the cooking game's Counter scene.
        SceneManager.LoadScene(counterSceneName);
    }

    private void OnDestroy()
    {
        if (startNewLevelButton != null)
        {
            startNewLevelButton.onClick.RemoveListener(StartNewLevel);
        }
    }
}
