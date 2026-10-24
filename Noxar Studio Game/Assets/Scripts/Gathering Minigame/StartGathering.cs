using UnityEngine;
using UnityEngine.InputSystem;

public class StartGathering : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public GameObject starsGathering;

    private void Awake()
    {
        if (TapSprite.patronHasOrdered)
            starsGathering.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");

        // Checks if patron has ordered before starting the gathering minigame
        if (TapSprite.patronHasOrdered)
        {
            canvas.SetActive(true);
            player.SetActive(true);
        }
        else
        {
            Debug.Log("Patron has not ordered yet.");
        }
    }
}
