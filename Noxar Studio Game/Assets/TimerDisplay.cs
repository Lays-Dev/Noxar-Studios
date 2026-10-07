
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TMP_Text timerText;

    private void Update()
    {
        if (GameTimer.Instance == null)
            return;

        float time = GameTimer.Instance.timeElapsed;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
}