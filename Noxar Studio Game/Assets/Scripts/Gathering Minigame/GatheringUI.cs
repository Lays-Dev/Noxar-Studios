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

    // Put your buttons here
    [SerializeField] private Button[] buttons;

    private int buttonsClicked = 0;

    void Start()
    {
        endMinigameButton.gameObject.SetActive(false);

        endMinigameButton.onClick.AddListener(EndMinigame);
        closeButton.onClick.AddListener(CloseInstructions);

        // Turn all buttons off at the start
        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(false);
            button.onClick.AddListener(ButtonClicked);
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

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "2";

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "1";

        yield return new WaitForSeconds(1f);

        countdownText.SetActive(false);
        glorboImage.SetActive(false);

        // Start the random button sequence
        ActivateRandomButton();
    }

    private void ActivateRandomButton()
    {
        // Find all buttons that haven't been clicked
        int remainingButtons = 0;

        foreach (Button button in buttons)
        {
            if (!button.gameObject.activeSelf)
            {
                remainingButtons++;
            }
        }

        if (remainingButtons == 0)
        {
            WinMinigame();
            return;
        }

        // Pick a random inactive button
        int randomIndex = Random.Range(0, buttons.Length);

        while (buttons[randomIndex].gameObject.activeSelf)
        {
            randomIndex = Random.Range(0, buttons.Length);
        }

        // Activate it
        buttons[randomIndex].gameObject.SetActive(true);
    }

    private void ButtonClicked()
    {
        buttonsClicked++;

        // Hide the button that was clicked
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current
            .currentSelectedGameObject.GetComponent<Button>();

        clickedButton.gameObject.SetActive(false);

        // Activate another random button
        ActivateRandomButton();
    }

    private void WinMinigame()
    {
        Debug.Log("Minigame Complete!");

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