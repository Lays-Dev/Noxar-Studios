using UnityEngine;
using TMPro;

public class GalaxyCubesUI : MonoBehaviour
{
    [Header("Currency Display")]
    public TMP_Text galaxyCubesText;

    private void Start()
    {
        UpdateGalaxyCubesUI();
    }

    public void UpdateGalaxyCubesUI()
    {
        if (LevelManager.Instance == null)
            return;

        galaxyCubesText.text =
            LevelManager.Instance.totalGalaxyCubes.ToString();
    }
}
