using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinigamePanel : MonoBehaviour
{
    [Header("Minigame Controls")]
    public Button completeButton;

    [Header("Completion Sound")]
    public AudioSource audioSource;
    public AudioClip completeSFX;

    private KitchenManager kitchenManager;
    private bool isCompleting = false;

    // Called by KitchenManager after this prefab is created.
    public void Setup(KitchenManager manager)
    {
        kitchenManager = manager;

        if (completeButton != null)
        {
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
        // Prevent the button from being clicked repeatedly.
        if (isCompleting)
            return;

        isCompleting = true;

        // Disable the button while the sound plays.
        if (completeButton != null)
        {
            completeButton.interactable = false;
        }

        StartCoroutine(PlaySoundThenComplete());
    }

    private IEnumerator PlaySoundThenComplete()
    {
        // Play the completion sound, if one is assigned.
        if (audioSource != null && completeSFX != null)
        {
            audioSource.PlayOneShot(completeSFX);

            // Wait for the full clip to finish.
            yield return new WaitForSecondsRealtime(
                completeSFX.length
            );
        }

        // Now tell the KitchenManager to complete the minigame.
        if (kitchenManager != null)
        {
            kitchenManager.CompleteCurrentMinigame();
        }
        else
        {
            Debug.LogWarning(
                "MinigamePanel: KitchenManager reference is missing.",
                this
            );
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

