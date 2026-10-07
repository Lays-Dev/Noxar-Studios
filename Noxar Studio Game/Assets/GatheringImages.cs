using UnityEngine;
using UnityEngine.UI;

public class GatheringImages : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private Button closeButton;

    [SerializeField] private TapImage tapImage;

    void Start()
    {
        closeButton.onClick.AddListener(CloseInstructionsPanel);
    }

    private void CloseInstructionsPanel()
    {
        instructionsPanel.SetActive(false);

        // Start the TapImage minigame
        tapImage.StartTapping();
    }
}