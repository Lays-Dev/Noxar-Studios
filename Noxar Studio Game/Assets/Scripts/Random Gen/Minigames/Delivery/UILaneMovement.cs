
using UnityEngine;
using UnityEngine.UI;

public class UILaneMovement : MonoBehaviour
{
    [Header("Player UI Image")]
    public RectTransform playerRect;

    [Header("Five Lane Y Positions")]
    public float[] lanePositions = { 351f, 170f, 0f, -170f, -351f };

    [Header("Movement")]
    public float moveSpeed = 800f;

    [Header("Ship Rotation")]
    public float rotationAngle = 20f;
    public float rotationSpeed = 180f;

    // The middle lane is the starting lane.
    private int currentLane = 2;

    // The lane the player is moving toward.
    private float targetY;

    // The rotation the ship is moving toward.
    private float targetRotation = 0f;

    private void Start()
    {
        if (playerRect == null)
        {
            playerRect = GetComponent<RectTransform>();
        }

        if (lanePositions == null || lanePositions.Length != 5)
        {
            Debug.LogError("Please set exactly five lane positions!");
            enabled = false;
            return;
        }

        // Start in the middle lane.
        targetY = lanePositions[currentLane];

        Vector2 position = playerRect.anchoredPosition;
        position.y = targetY;
        playerRect.anchoredPosition = position;

        playerRect.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        // Smoothly move toward the selected lane.
        Vector2 position = playerRect.anchoredPosition;

        position.y = Mathf.MoveTowards(
            position.y,
            targetY,
            moveSpeed * Time.deltaTime
        );

        playerRect.anchoredPosition = position;

        // Smoothly rotate the ship.
        Quaternion desiredRotation = Quaternion.Euler(
            0f,
            0f,
            targetRotation
        );

        playerRect.localRotation = Quaternion.RotateTowards(
            playerRect.localRotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // Called by the UP button.
    public void MoveUp()
    {
        if (currentLane > 0)
        {
            currentLane--;
            targetY = lanePositions[currentLane];

            // Tilt ship upward.
            targetRotation = rotationAngle;
        }
    }

    // Called by the DOWN button.
    public void MoveDown()
    {
        if (currentLane < 4)
        {
            currentLane++;
            targetY = lanePositions[currentLane];

            // Tilt ship downward.
            targetRotation = -rotationAngle;
        }
    }
}
