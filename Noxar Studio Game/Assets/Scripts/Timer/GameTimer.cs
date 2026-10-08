
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float timeElapsed = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }
}
