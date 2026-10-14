using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    // Remembers whether Level 2 has been unlocked
    public static bool level2Unlocked = false;

    private void Awake()
    {
        // Prevent duplicate managers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keep this manager when changing scenes
        DontDestroyOnLoad(gameObject);

        Debug.Log("LevelManager is persistent.");
    }

    public void UnlockLevel2()
    {
        level2Unlocked = true;
        Debug.Log("Level 2 unlocked!");

    }
}