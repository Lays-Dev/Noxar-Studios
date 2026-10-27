
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

    // The middle lane is the starting lane.
    private int currentLane = 2;

    // The lane the player is moving toward.
    private float targetY;

    private void Start()
    {
        if (playerRect == null)
        {
            playerRect = GetComponent<RectTransform>();
        }

        // Make sure exactly five lanes are configured.
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
    }

    // Called by the UP button.
    public void MoveUp()
    {
        // Lane 0 is the top lane.
        if (currentLane > 0)
        {
            currentLane--;
            targetY = lanePositions[currentLane];
        }
    }

    // Called by the DOWN button.
    public void MoveDown()
    {
        // Lane 4 is the bottom lane.
        if (currentLane < 4)
        {
            currentLane++;
            targetY = lanePositions[currentLane];
        }
    }
}
