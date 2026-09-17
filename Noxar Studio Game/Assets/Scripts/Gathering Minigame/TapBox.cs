using UnityEngine;

public class TapBox : MonoBehaviour
{
    [SerializeField] private int tapsRequired = 5;

    private int tapsRemaining;
    private GatheringUI gatheringUI;

    private void Start()
    {
        gatheringUI = FindFirstObjectByType<GatheringUI>();
    }

    private void OnEnable()
    {
        tapsRemaining = tapsRequired;
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