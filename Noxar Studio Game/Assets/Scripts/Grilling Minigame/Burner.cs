
using UnityEngine;
using UnityEngine.UI;

// Burner states
public enum BurnerState
{
    Cold,
    Hot
}

public class Burner : MonoBehaviour
{

    // Choose hot or cold burner in the Inspector
    public BurnerState state = BurnerState.Cold;

    [Header("Burner Images")]
    public Image burnerImage;
    public Sprite coldSprite;
    public Sprite hotSprite;

    // How many seconds before the state changes
    public float switchInterval = 5f;

    // Timer that counts down state change
    private float timer;

     private void Start()
    {
        // Start the timer
        timer = switchInterval;

        // Show the correct image at the start
        UpdateBurnerImage();
    }

    private void Update()
    {
        // Count down every second
        timer -= Time.deltaTime;

        // Switch state when the timer reaches zero
        if (timer <= 0f)
        {
            SwitchState();

            // Reset the timer
            timer = switchInterval;
        }
    }

    // Change from Hot to Cold or Cold to Hot
    private void SwitchState()
    {
        if (state == BurnerState.Hot)
        {
            state = BurnerState.Cold;
        }
        else
        {
            state = BurnerState.Hot;
        }

        // Update the image whenever the state changes
        UpdateBurnerImage();

        Debug.Log(gameObject.name + " is now " + state);
    }

 // Change the UI image to match the current state
    private void UpdateBurnerImage()
    {
        if (burnerImage == null)
            return;

        if (state == BurnerState.Hot)
        {
            burnerImage.sprite = hotSprite;
        }
        else
        {
            burnerImage.sprite = coldSprite;
        }
    }

    // Called when the pot is dropped on this burner
    public void AcceptDrop()
    {
        Debug.Log("The pot was placed on the burner.");

        // Check the burner's current state
        if (state == BurnerState.Hot)
        {
            Debug.Log("The burner is HOT!");
        }
        else
        {
            Debug.Log("The burner is COLD!");
        }
    }
}

