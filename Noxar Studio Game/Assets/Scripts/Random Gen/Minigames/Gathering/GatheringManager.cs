using UnityEngine;

public class GatheringManager : MonoBehaviour
{
    public GameObject gatheringBackgroundClosedFridge;

    public void OpenClosedFridgeBackground()
    {
        gatheringBackgroundClosedFridge.SetActive(false);
    }
}
