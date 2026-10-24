using UnityEngine;
// Removed extra using statements


public class PlayerAnimations : MonoBehaviour

{
    // Connect the animaator component and movement script. Attach Player GameObject
    [SerializeField] private Animator animator;
    [SerializeField] private movement playerMovement;

    // Removed extra variables lastYposition, targetposition, spriteRenderer, rigidbody

    // Update usually goes after the start method
    void Update()
    {
        // Check for movement every frame
        CheckMovement();
    }

    void CheckMovement()
    {
        
        // Reads the vertical input from the movement script and stores it in yInput
        //yInput is used to determine if the player is moving up or down, it can be named anything
        float yInput = playerMovement.MoveInput.y;

       // Debug.Log("Y Input: " + yInput);

        // Checks if the player is moving up
        if (yInput > 0.1f)
        {
            // Walking up
            animator.SetBool("isRunningUp", true);
            animator.SetBool("isRunningDown", false);
        }
        // Checks if the player is moving down
        else if (yInput < -0.1f)
        {
            // Walking down
            animator.SetBool("isRunningUp", false);
            animator.SetBool("isRunningDown", true);
        }
        // Checks if the player is not moving vertically
        else
        {
            // Idle or walking horizontally
            animator.SetBool("isRunningUp", false);
            animator.SetBool("isRunningDown", false);
        }
    }
}


