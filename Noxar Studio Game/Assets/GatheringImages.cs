using UnityEngine;
using UnityEngine.UI;

public class GatheringImages : MonoBehaviour
{

// This panel will close later when a button is pressed.
[SerializeField] private GameObject instructionsPanel;
[SerializeField] private Button closeButton;

    void Start()
    {
        //endMinigameButton.gameObject.SetActive(false);
        closeButton.onClick.AddListener(CloseInstructionsPanel);
    }

    private void CloseInstructionsPanel()
    {
        instructionsPanel.SetActive(false);
    }
}
