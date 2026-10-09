using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float timeRemaining = 40f;
    public TMP_Text timerText;
    public GameObject losePanel;

    private bool timerFinished = false;

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
        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                timerFinished = true;
            }
        }

        // Update the timer text
        if (timerText != null)
        {
            timerText.text =
                Mathf.CeilToInt(timeRemaining).ToString();
        }

        // Show the lose panel when the timer ends
        if (timerFinished && losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    public void ResetTimer()
    {
        timeRemaining = 40f;
        timerFinished = false;

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    public void StartNewDay() 
    {
        // Reset the timer
        GameTimer.Instance.ResetTimer();

        // Reload the current scene
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}
