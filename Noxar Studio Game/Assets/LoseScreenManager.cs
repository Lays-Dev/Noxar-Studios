using UnityEngine;

public class LoseScreenManager : MonoBehaviour
{
    public static LoseScreenManager Instance;

    private void Awake()
    {
        Debug.Log("LoseScreenManager Awake: " + gameObject.name);

        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate LoseScreenManager destroyed!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("Lose Screen Canvas set to persist.");
    }

    private void OnDestroy()
    {
        Debug.LogWarning("Lose Screen Canvas was destroyed!");

        if (Instance == this)
        {
            Instance = null;
        }
    }
}
