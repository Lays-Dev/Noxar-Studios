
using UnityEngine;

public class MagnetClaw : MonoBehaviour
{
    [Header("Magnet Settings")]
    public Transform magnetPoint;
    public Transform capsuleContainer;

    public float magnetRange = 6f;
    public float attractionSpeed = 5f;
    public float attachDistance = 0.15f;

    [Header("Button References")]
    public PayButton payButton;

    private Capsule targetCapsule;
    private Rigidbody2D targetBody;
    private Collider2D[] targetColliders;

    private bool isAttracting;
    private bool isAttached;

    public void MagnetizeClosestCapsule()
    {
        // Don't grab another capsule while carrying one.
        if (isAttracting || isAttached)
            return;

        if (magnetPoint == null || capsuleContainer == null)
        {
            Debug.LogWarning("Magnet references are missing!");
            return;
        }

        Capsule[] capsules =
            capsuleContainer.GetComponentsInChildren<Capsule>();

        float closestDistance = magnetRange;
        Capsule closestCapsule = null;

        // Find the closest capsule.
        foreach (Capsule capsule in capsules)
        {
            if (capsule == null)
                continue;

            float distance = Vector2.Distance(
                magnetPoint.position,
                capsule.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCapsule = capsule;
            }
        }

        if (closestCapsule == null)
        {
            Debug.Log("No capsules within magnet range!");
            return;
        }

        targetCapsule = closestCapsule;

        targetBody = targetCapsule.GetComponent<Rigidbody2D>();

        targetColliders =
            targetCapsule.GetComponentsInChildren<Collider2D>();

        // Stop capsule physics.
        if (targetBody != null)
        {
            targetBody.linearVelocity = Vector2.zero;
            targetBody.gravityScale = 0f;
        }

        // Disable collisions while being attracted.
        foreach (Collider2D col in targetColliders)
        {
            col.enabled = false;
        }

        isAttracting = true;

        Debug.Log("Magnet activated!");
    }

    private void FixedUpdate()
    {
        if (targetCapsule == null)
            return;

        if (isAttached)
            return;

        if (!isAttracting)
            return;

        Vector2 currentPosition = targetCapsule.transform.position;
        Vector2 targetPosition = magnetPoint.position;

        // Pull capsule toward magnet.
        Vector2 newPosition = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            attractionSpeed * Time.fixedDeltaTime
        );

        if (targetBody != null)
        {
            targetBody.MovePosition(newPosition);
        }
        else
        {
            targetCapsule.transform.position = newPosition;
        }

        // Attach when close enough.
        if (Vector2.Distance(newPosition, targetPosition)
            <= attachDistance)
        {
            AttachCapsule();
        }
    }

    private void AttachCapsule()
    {
        // Stop physics simulation.
        if (targetBody != null)
        {
            targetBody.linearVelocity = Vector2.zero;
            targetBody.simulated = false;
        }

        // Attach capsule to claw.
        targetCapsule.transform.SetParent(magnetPoint);

        targetCapsule.transform.localPosition = Vector3.zero;

        isAttracting = false;
        isAttached = true;

        Debug.Log("Capsule attached to claw!");

        // Deactivate the Pay Button after grabbing a capsule.
        if (payButton != null)
        {
            payButton.DeactivateButton();
        }
    }
}
