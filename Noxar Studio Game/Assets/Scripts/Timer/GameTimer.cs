
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float timeRemaining = 40f;
    public GameObject losePanel;

    public bool timerRunning = true;

    private bool timerFinished = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the lose panel in the new scene
        GameObject foundPanel = GameObject.Find("LoseScreen");

        if (foundPanel != null)
        {
            losePanel = foundPanel;
            losePanel.SetActive(timerFinished);
        }
    }

    private void Update()
    {
        // Only count down while the timer is running
        if (timerRunning && timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerFinished = true;
                timerRunning = false;

                if (losePanel != null)
                    losePanel.SetActive(true);
            }
        }
    }

    public void PauseTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        if (!timerFinished)
            timerRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = 40f;
        timerFinished = false;
        timerRunning = true;

        if (losePanel != null)
            losePanel.SetActive(false);
    }
}
