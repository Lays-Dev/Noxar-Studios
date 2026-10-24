using UnityEngine;

public class ButtonPulseManager : MonoBehaviour
{
    public GameObject deliverButton;

    [Header("Pulse Settings")]
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.1f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = deliverButton.transform.localScale;

        // Hide the button until the required tasks are complete.
        deliverButton.SetActive(true);
    }

    private void Update()
    {
        bool tasksComplete;

        if (LevelManager.level2Unlocked)
        {
            // Level 2: Gathering AND grilling must be complete.
            tasksComplete =
                EndMinigame.gatherComplete &&
                GrillingUIManager.grillingCompleted;
        }
        else
        {
            // Level 1: Only gathering must be complete.
            tasksComplete = EndMinigame.gatherComplete;
        }

        if (tasksComplete)
        {
            // Show the button.
            if (!deliverButton.activeSelf)
            {
                deliverButton.SetActive(true);
            }

            // Pulse the button.
            float pulse = 1f +
                Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

            deliverButton.transform.localScale =
                originalScale * pulse;
        }
        else
        {
            // Hide the button if the requirements are not met.
            if (deliverButton.activeSelf)
            {
                deliverButton.SetActive(true);
            }
        }
    }

}
