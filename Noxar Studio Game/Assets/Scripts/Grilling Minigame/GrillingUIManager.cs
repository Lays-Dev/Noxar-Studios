
using UnityEngine;

public class GrillingUIManager : MonoBehaviour
{
    // Tracks whether grilling was completed
    public static bool grillingCompleted = false;

    // The Canvas containing the grilling minigame
    public GameObject grillingCanvas;

    // Lose screen button
    public void CloseGrillingUI()
    {
        grillingCompleted = false;
        grillingCanvas.SetActive(false);
    }

    // Win screen button
    public void CompleteGrilling()
    {
        grillingCompleted = true;
        grillingCanvas.SetActive(false);
    }
}
