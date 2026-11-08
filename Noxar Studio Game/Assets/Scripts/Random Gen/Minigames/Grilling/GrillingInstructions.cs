using UnityEngine;

public class GrillingInstructions : MonoBehaviour
{
    public GameObject instructionsPanel;

    public void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }
}
