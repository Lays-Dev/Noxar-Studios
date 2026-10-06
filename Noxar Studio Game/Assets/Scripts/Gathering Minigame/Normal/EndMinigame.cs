using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class EndMinigame : MonoBehaviour
{
    // This panel will close later when a button is pressed.
[SerializeField] private GameObject instructionsPanel;
//[SerializeField] private GameObject tapBox;
[SerializeField] private Button endMinigameButton;
[SerializeField] private Button startButton;
[SerializeField] private GameObject openFridgeImage;
[SerializeField] private GameObject minigameCanvas;
public static bool gatherComplete = false;





void Start()
{
    //endMinigameButton.gameObject.SetActive(false);
    endMinigameButton.onClick.AddListener(End);
    startButton.onClick.AddListener(StartMinigame);
}
    // Function that closes the panel - used for a button.
    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
        //Countdown();
    }


private void End()
{
    //tapBox.SetActive(false);
    //endMinigameButton.gameObject.SetActive(false);
    //minigamePanel.SetActive(false);
    gatherComplete = true;
//    openFridgeImage.SetActive(false);
    minigameCanvas.SetActive(false);
}
    
private void StartMinigame()
{
    instructionsPanel.SetActive(false);
    openFridgeImage.SetActive(true);
}
}