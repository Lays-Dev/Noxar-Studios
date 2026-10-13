using UnityEngine;

public class NewDay : MonoBehaviour
{
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject dish;
    public GameObject endDayButton;
    public GameObject currency;
    public GameObject galico;
    public GameObject johnny;


    // Check which screen the button belongs to
    public bool comingFromWinScreen = false;

    public static bool level2 = false;

    public void StartNewDay()
    {
        // Only advance to level 2 from the win screen
        if (comingFromWinScreen && !level2)
        {
            level2 = true;
            Debug.Log("Level 2 activated!");
        }

        Debug.Log("Level 2 is: " + level2);

        // Reset the timer to 40 seconds
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.ResetTimer();
        }

        // Reset patron order status
        TapSprite.patronHasOrdered = false;

        // Reset gathering status
        EndMinigame.gatherComplete = false;

        // Hide the lose screen
        if (loseScreen != null)
        {
            loseScreen.SetActive(false);
        }

        // Hide the win screen
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }

        // Hide the end day button
        if (endDayButton != null)
        {
            endDayButton.SetActive(false);
        }

        // Hide the dish
        if (dish != null)
        {
            dish.SetActive(false);
        }

        if (currency != null)
        {
            currency.SetActive(false);
        }

        if (galico != null)
        {
            galico.SetActive(false);
        }

        johnny.SetActive(true);
        
    }
}