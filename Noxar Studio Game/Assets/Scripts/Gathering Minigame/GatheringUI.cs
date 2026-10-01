using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GatheringUI : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject countdownText;
    [SerializeField] private Button endMinigameButton;
    [SerializeField] private GameObject openFridgeImage;
    [SerializeField] private GameObject minigamePanel;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject glorboImage;

    // Parent object containing the 4 minigame buttons
    [SerializeField] private Transform buttonParent;

    // The buttons will be found automatically
    private Button[] buttons;

    [SerializeField] private int clicksRequired = 4;

    private bool[] buttonCompleted;

    private Button currentButton;

    private int clicksRemaining;

    private int buttonsCompleted = 0;


private void Start()
{
    endMinigameButton.gameObject.SetActive(false);

    endMinigameButton.onClick.AddListener(EndMinigame);
    closeButton.onClick.AddListener(CloseInstructions);

    // Find the 4 buttons
    buttons = buttonParent.GetComponentsInChildren<Button>(true);

    Debug.Log("Number of buttons found: " + buttons.Length);

    // Create completed array
    buttonCompleted = new bool[buttons.Length];

    // Turn all buttons off
    for (int i = 0; i < buttons.Length; i++)
    {
        int buttonIndex = i;

        buttons[i].gameObject.SetActive(false);

        // Tell this specific button which button it is
        buttons[i].onClick.AddListener(() => ButtonClicked(buttonIndex));
    }
}


    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);

        StartCoroutine(Countdown());
    }


    private IEnumerator Countdown()
    {
        countdownText.SetActive(true);

        countdownText.GetComponent<TMP_Text>().text = "3";

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "2";

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "1";

        yield return new WaitForSeconds(1f);

        countdownText.SetActive(false);

        glorboImage.SetActive(false);

        // Start the button minigame
        ActivateRandomButton();
    }


    private void ActivateRandomButton()
    {
        Debug.Log("Buttons completed: " + buttonsCompleted);
        Debug.Log("Buttons available: " + buttons.Length);

        // Make sure there are buttons
        if (buttons.Length == 0)
        {
            Debug.LogError("No buttons were found!");
            return;
        }

        // Check if all 4 buttons have been completed
        if (buttonsCompleted >= buttons.Length)
        {
            WinMinigame();
            return;
        }

        int randomIndex;

        // Pick a button that has not been completed
        do
        {
            randomIndex = Random.Range(0, buttons.Length);
        }
        while (buttonCompleted[randomIndex]);

        // Set the selected button as the current button
        currentButton = buttons[randomIndex];

        // Reset clicks
        clicksRemaining = clicksRequired;

        // Activate the button
        currentButton.gameObject.SetActive(true);

        Debug.Log("Button " + randomIndex + " activated!");
    }


private void ButtonClicked(int buttonIndex)
{
    // Make sure the correct button was clicked
    if (buttons[buttonIndex] != currentButton)
    {
        return;
    }

    clicksRemaining--;

    Debug.Log("Button " + buttonIndex + " clicked!");
    Debug.Log("Clicks remaining: " + clicksRemaining);

    if (clicksRemaining <= 0)
    {
        // Mark this button as completed
        buttonCompleted[buttonIndex] = true;

        // Hide the button
        buttons[buttonIndex].gameObject.SetActive(false);

        // Increase completed count
        buttonsCompleted++;

        Debug.Log("Button completed!");

        // Choose another button
        ActivateRandomButton();
    }
}


    private void WinMinigame()
    {
        Debug.Log("Minigame Complete!");

        if (currentButton != null)
        {
            currentButton.gameObject.SetActive(false);
        }

        openFridgeImage.SetActive(false);

        endMinigameButton.gameObject.SetActive(true);
    }


    private void EndMinigame()
    {
        endMinigameButton.gameObject.SetActive(false);

        minigamePanel.SetActive(false);

        openFridgeImage.SetActive(false);
    }
}