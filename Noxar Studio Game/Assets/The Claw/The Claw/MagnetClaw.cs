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

    [Header("Collection UI")]
    public GameObject collectionCanvas;
    public CapsuleCollectionUI collectionUI;

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
        CapsuleData collectedData = targetCapsule.GetCapsuleData();

        if (targetBody != null)
        {
            targetBody.linearVelocity = Vector2.zero;
            targetBody.simulated = false;
        }

        targetCapsule.transform.SetParent(magnetPoint);
        targetCapsule.transform.localPosition = Vector3.zero;

        isAttracting = false;
        isAttached = true;

        Debug.Log("Capsule attached to claw!");

        if (payButton != null)
            payButton.DeactivateButton();

        // Activate the entire collection Canvas.
        if (collectionCanvas != null)
            collectionCanvas.SetActive(true);

        // Display the capsule's image and name.
        if (collectionUI != null)
            collectionUI.ShowCapsule(collectedData);
    }


    public void ResetClawMachine()
    {
        Debug.Log("Resetting claw machine!");

        // Destroy any capsule currently attached to the magnet.
        if (magnetPoint != null)
        {
            Capsule[] heldCapsules =
                magnetPoint.GetComponentsInChildren<Capsule>(true);

            foreach (Capsule capsule in heldCapsules)
            {
                if (capsule != null)
                {
                    Destroy(capsule.gameObject);
                    Debug.Log("Destroyed captured capsule: " + capsule.name);
                }
            }
        }

        // Destroy the target capsule if it is not already
        // being destroyed as a child of the magnet.
        if (targetCapsule != null)
        {
            if (magnetPoint == null ||
                !targetCapsule.transform.IsChildOf(magnetPoint))
            {
                Destroy(targetCapsule.gameObject);
            }
        }

        // Clear the capsule references.
        targetCapsule = null;
        targetBody = null;
        targetColliders = null;

        // Reset the claw states.
        isAttracting = false;
        isAttached = false;

        // Hide the collection screen.
        if (collectionUI != null)
            collectionUI.CloseCollectionUI();

        if (collectionCanvas != null)
            collectionCanvas.SetActive(false);

        Debug.Log("Claw machine reset complete!");
    }


}
