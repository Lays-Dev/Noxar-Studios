
using UnityEngine;
using TMPro;

public class GalaxyCubeUI : MonoBehaviour
{
    [Header("Galaxy Cube UI")]
    public TMP_Text galaxyCubeText;

    private int displayedAmount = -1;

    private void Update()
    {
        // Find the LevelManager.
        if (LevelManager.Instance == null)
            return;

        // Get the player's current Galaxy Cube total.
        int currentAmount = LevelManager.Instance.totalGalaxyCubes;

        // Only update the UI when the amount changes.
        if (currentAmount != displayedAmount)
        {
            displayedAmount = currentAmount;

            if (galaxyCubeText != null)
            {
                galaxyCubeText.text = currentAmount.ToString();
            }
        }
    }
}
