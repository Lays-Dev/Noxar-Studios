using UnityEngine;

public class GatheringUI : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }
}
