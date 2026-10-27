using UnityEngine;
using UnityEngine.UI;

public class MinigamePanel : MonoBehaviour
{
    [Header("Minigame Controls")]
    public Button completeButton;

    private KitchenManager kitchenManager;

    // Called by KitchenManager after this prefab is created.
    public void Setup(KitchenManager manager)
    {
        kitchenManager = manager;

        if (completeButton != null)
        {
            // Avoid adding the same listener twice.
            completeButton.onClick.RemoveListener(CompleteMinigame);
            completeButton.onClick.AddListener(CompleteMinigame);
        }
        else
        {
            Debug.LogWarning(
                "MinigamePanel: Assign the Complete Button in the Inspector.",
                this
            );
        }
    }

    private void CompleteMinigame()
    {
        if (kitchenManager != null)
        {
            kitchenManager.CompleteCurrentMinigame();
        }
    }

    private void OnDestroy()
    {
        if (completeButton != null)
        {
            completeButton.onClick.RemoveListener(CompleteMinigame);
        }
    }
}
