
using UnityEngine;

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

    // How many seconds before the state changes
    public float switchInterval = 5f;

    // Timer that counts down state change
    private float timer;

     private void Start()
    {
        // Start the timer
        timer = switchInterval;
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

        Debug.Log(gameObject.name + " is now " + state);
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

