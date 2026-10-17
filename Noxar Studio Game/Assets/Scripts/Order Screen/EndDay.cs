using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EndDay : MonoBehaviour
{
    public GameObject endDayImage;
    public GameObject winCanvas;

   public void EndOfDay()
    {
        endDayImage.SetActive(true);
    }

    public void WinCanvas()
    {
        winCanvas.SetActive(true);
    }
}
    
    

    
