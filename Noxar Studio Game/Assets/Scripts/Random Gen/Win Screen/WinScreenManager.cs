
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    [Header("Galaxy Cube UI")]
    public TMP_Text galaxyCubesText;
    public Image galaxyCubesImage;

    private void Start()
    {
        // Get the existing LevelManager.
        if (LevelManager.Instance == null)
        {
            Debug.LogError("WinScreenManager: LevelManager not found!");
            return;
        }

        // Get the amount of Galaxy Cubes earned.
        int amountEarned =
            LevelManager.Instance.galaxyCubesEarned;

        // Update the text.
        if (galaxyCubesText != null)
        {
            galaxyCubesText.text =
                "+" + amountEarned + " Galaxy Cubes";
        }
        else
        {
            Debug.LogError("Galaxy Cubes Text is not assigned!");
        }

        // Show the Galaxy Cube image.
        if (galaxyCubesImage != null)
        {
            galaxyCubesImage.gameObject.SetActive(true);
        }

        Debug.Log("Win Screen displaying " + amountEarned + " Galaxy Cubes!");
    }
}
