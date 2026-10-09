
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    private TMP_Text timerText;

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (GameTimer.Instance == null)
            return;

        timerText.text = Mathf.CeilToInt(
            GameTimer.Instance.timeRemaining).ToString();
    }
}
