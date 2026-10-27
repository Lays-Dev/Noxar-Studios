
using UnityEngine;

public class DeliveryPlayer : MonoBehaviour
{
    [Header("Lane Settings")]
    public float[] lanePositions = { 351f, 170f, 0f, -170f, -351f };

    [Header("Movement")]
    public float moveSpeed = 1000f;

    private RectTransform rectTransform;
    private int currentLane = 2; // Start in the middle lane

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // Place the player in the starting lane.
        SetLanePosition();
    }

    private void Update()
    {
        // Move smoothly toward the selected lane.
        Vector2 targetPosition = new Vector2(
            rectTransform.anchoredPosition.x,
            lanePositions[currentLane]
        );

        rectTransform.anchoredPosition = Vector2.MoveTowards(
            rectTransform.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    // Connect this to the Up button.
    public void MoveUp()
    {
        if (currentLane > 0)
        {
            currentLane--;
        }
    }

    // Connect this to the Down button.
    public void MoveDown()
    {
        if (currentLane < lanePositions.Length - 1)
        {
            currentLane++;
        }
    }

    private void SetLanePosition()
    {
        Vector2 position = rectTransform.anchoredPosition;
        position.y = lanePositions[currentLane];
        rectTransform.anchoredPosition = position;
    }
}
