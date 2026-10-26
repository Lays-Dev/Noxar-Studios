/*
using UnityEngine;
using UnityEngine.UI;

public class EndMinigame : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private Button endMinigameButton;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject openFridgeImage;
    [SerializeField] private GameObject minigameCanvas;

    public static bool gatherComplete = false;

    private void Start()
    {
        endMinigameButton.onClick.AddListener(End);
        startButton.onClick.AddListener(StartMinigame);
    }

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }


    private void End()
    {
        // Gathering was completed successfully.
        gatherComplete = true;

        // Record gathering only for the phone order.
        if (LevelManager.level2Unlocked &&
            TapSprite.phoneOrderTaken)
        {
            TapSprite.phoneOrderGatherComplete = true;

            Debug.Log("Phone order gathering completed!");
        }

        minigameCanvas.SetActive(false);
    }


    private void StartMinigame()
    {
        instructionsPanel.SetActive(false);
        openFridgeImage.SetActive(true);
    }
}
*/
