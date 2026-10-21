using UnityEngine;

public class DeliveryPlayer : MonoBehaviour
{
    [Header("Lane Settings")]
    public float topY = 4f;
    public float bottomY = -4f;

    // Five available lanes.
    private int currentLane = 2;

    private float laneSpacing;

    private void Start()
    {
        // Calculate the distance between lanes.
        laneSpacing = (topY - bottomY) / 4f;

        // Start in the middle lane.
        currentLane = 2;
        UpdateLanePosition();
    }

    public void MoveUp()
    {
        // Move up one lane.
        if (currentLane < 4)
        {
            currentLane++;
            UpdateLanePosition();
        }
    }

    public void MoveDown()
    {
        // Move down one lane.
        if (currentLane > 0)
        {
            currentLane--;
            UpdateLanePosition();
        }
    }

    private void UpdateLanePosition()
    {
        float newY = bottomY + currentLane * laneSpacing;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}
