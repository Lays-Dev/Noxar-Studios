using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerAnimations : MonoBehaviour

{
[SerializeField] private SpriteRenderer spriteRenderer;
[SerializeField] private Animator animator;
public Rigidbody2D rb;
private float lastYPosition;
public Vector3 targetPosition;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
    }

    void movement()

    {
        if (rb.linearVelocity.y > 0f)
        {
            animator.SetBool("isRunningUp", true);
        }
        else if (rb.linearVelocity.y < 0f)
        {
            animator.SetBool("isRunningDown", true);
        }
        else
        {
            Debug.Log("idle");

        }
    }

// Update is called once per frame
void Update()
{
    movement();
        float yVelocity = 0f;
        bool isMovingUp = yVelocity > 0.1f;
        bool isMovingDown = yVelocity < -0.1f;
    }
}
