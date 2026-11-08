using UnityEngine;

public class NewLevelButton : MonoBehaviour
{
    public void StartNewLevel()
    {
        // Get the persistent LevelManager.
        LevelManager levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found!");
            return;
        }

        // Start the next level.
        levelManager.StartNextLevel();

        Debug.Log("New Level Started!");
    }
}