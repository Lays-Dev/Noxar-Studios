/*using UnityEngine;

public class TimerPanelTrigger : MonoBehaviour
{
    public GameObject panel;

    private bool panelActivated = false;

    private void Update()
    {
        if (GameTimer.Instance == null)
            return;

        // Activate panel after 40 seconds
        if (GameTimer.Instance.timeElapsed >= 40f && !panelActivated)
        {
            panel.SetActive(true);
            panelActivated = true;
        }
    }
}*/
