using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float timeElapsed = 0f;

    private void Awake()
    {
        // Prevent multiple timers from being created
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keep this timer when changing scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }
}
