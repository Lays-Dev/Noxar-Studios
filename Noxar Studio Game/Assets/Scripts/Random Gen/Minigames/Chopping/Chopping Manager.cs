using UnityEngine;
using System.Collections;

public class ChoppingManager : MonoBehaviour
{
    [Header("Instructions")]
    public GameObject instructionsPanel;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip closeInstructionsSFX;

    [Header("Chopping")]
    public PointerController pointerController;

    public void CloseInstructions()
    {
        StartCoroutine(CloseInstructionsRoutine());
    }

    IEnumerator CloseInstructionsRoutine()
    {
        // Play the closing sound
        audioSource.PlayOneShot(closeInstructionsSFX);

        // Wait for the sound to finish
        yield return new WaitForSecondsRealtime(closeInstructionsSFX.length);

        // Close instructions
        instructionsPanel.SetActive(false);

        // Allow chopping input
        pointerController.canChop = true;
    }
}