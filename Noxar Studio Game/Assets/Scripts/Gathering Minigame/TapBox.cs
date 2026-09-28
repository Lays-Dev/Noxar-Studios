using UnityEngine;

public class TapBox : MonoBehaviour
{
    [SerializeField] private int tapsRequired = 5;
    [SerializeField] private float moveSpeed = 200f;
    [SerializeField] private RectTransform minigameArea;

    private int tapsRemaining;
    private GatheringUI gatheringUI;
    private RectTransform box;
    private int direction = 1;

    private void Start()
    {
        gatheringUI = FindFirstObjectByType<GatheringUI>();
        box = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        tapsRemaining = tapsRequired;
        direction = 1;
    }

    private void Update()
    {
        float halfWidth = box.rect.width / 2;

        float leftEdge = -minigameArea.rect.width / 2 + halfWidth;
        float rightEdge = minigameArea.rect.width / 2 - halfWidth;

        box.anchoredPosition += Vector2.right * moveSpeed * direction * Time.deltaTime;

        if (box.anchoredPosition.x >= rightEdge)
        {
            box.anchoredPosition = new Vector2(rightEdge, box.anchoredPosition.y);
            direction = -1;
        }

        if (box.anchoredPosition.x <= leftEdge)
        {
            box.anchoredPosition = new Vector2(leftEdge, box.anchoredPosition.y);
            direction = 1;
        }
    }

    public void Tap()
    {
        Debug.Log("TapBox Button Tapped!");

        tapsRemaining--;

        if (tapsRemaining <= 0)
        {
            gameObject.SetActive(false);
            gatheringUI.SpawnNewBox();
        }
    }
}