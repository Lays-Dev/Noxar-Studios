/*
using UnityEngine;

public class GrillingUIManager : MonoBehaviour
{
    public static bool grillingCompleted = false;

    public GameObject grillingCanvas;

    // Called by the lose/cancel button.
    public void CloseGrillingUI()
    {
        // Do not reset grillingCompleted here.
        // Closing the UI should not erase a previous win.

        if (grillingCanvas != null)
            grillingCanvas.SetActive(false);
    }

    // Called by the win button.

    public void CompleteGrilling()
    {
        grillingCompleted = true;

        // Record grilling only for the phone order.
        if (LevelManager.level2Unlocked &&
            TapSprite.phoneOrderTaken)
        {
            TapSprite.phoneOrderGrillComplete = true;

            Debug.Log("Phone order grilling completed!");
        }

        if (grillingCanvas != null)
            grillingCanvas.SetActive(false);
    }

}
*/