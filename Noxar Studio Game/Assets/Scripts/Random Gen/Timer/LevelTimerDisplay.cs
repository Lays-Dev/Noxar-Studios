
using UnityEngine;
using TMPro;

public class LevelTimerDisplay : MonoBehaviour
{
    [Header("Timer Text")]
    public TMP_Text timerText;

    private void Update()
    {
        // Find the persistent LevelManager.
        if (LevelManager.Instance == null || timerText == null)
            return;

        // Read the remaining time.
        float time = LevelManager.Instance.timeRemaining;

        // Round up so 29.2 seconds displays as 0:30.
        int totalSeconds = Mathf.CeilToInt(time);

        // Convert seconds into minutes and seconds.
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        // Display as 1:30, 0:45, 0:09, etc.
        timerText.text = string.Format(
            "{0}:{1:00}",
            minutes,
            seconds
        );
    }
}
