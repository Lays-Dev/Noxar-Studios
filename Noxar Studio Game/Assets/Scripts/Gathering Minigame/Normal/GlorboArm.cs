
using UnityEngine;

public class GlorboArm : MonoBehaviour
{
    //public float moveDistance = 50f;
    public float moveSpeed = 1f;

    private RectTransform arm;
    private Vector2 startPosition;

    private void Start()
    {
        arm = GetComponent<RectTransform>();
        startPosition = arm.anchoredPosition;
    }

    private void Update()
    {
        // Move the arm up and down repeatedly.
        // Rotate back and forth
        float rotation = Mathf.Lerp(
            -30f,
            30f,
            Mathf.PingPong(Time.time * moveSpeed, 1f)
        );

        arm.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }

}
