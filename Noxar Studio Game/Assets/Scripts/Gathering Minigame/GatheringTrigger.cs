using UnityEngine;

public class GatheringTrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiPanel.SetActive(true);
        }
    }
}