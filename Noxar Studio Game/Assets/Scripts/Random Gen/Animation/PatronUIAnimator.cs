using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PatronUIAnimator : MonoBehaviour
{
    [Header("Patron UI Image")]
    [SerializeField] private Image patronImage;

    private PatronData currentPatron;

    private Coroutine animationCoroutine;


    // Called when a new patron is displayed.
    public void SetPatron(PatronData patron)
    {
        if (patron == null)
            return;

        currentPatron = patron;

        // Stop any animation belonging to the previous patron.
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        // Start this patron's idle animation.
        PlayIdle();
    }


    // Plays the talking animation once.
    public void SetTalking()
    {
        if (currentPatron == null)
            return;

        if (currentPatron.talkingFrames == null ||
            currentPatron.talkingFrames.Length == 0)
        {
            Debug.LogWarning(
                currentPatron.patronName + " has no talking animation frames."
            );

            return;
        }

        // Stop the idle animation.
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        // Start the talking animation.
        animationCoroutine = StartCoroutine(PlayTalkingAnimation());
    }


    // Starts the idle animation.
    private void PlayIdle()
    {
        if (currentPatron == null)
            return;

        if (currentPatron.idleFrames == null ||
            currentPatron.idleFrames.Length == 0)
        {
            // Use the normal patron sprite if no idle frames are assigned.
            if (patronImage != null)
                patronImage.sprite = currentPatron.patronSprite;

            return;
        }

        animationCoroutine = StartCoroutine(PlayIdleAnimation());
    }


    // Loops the idle animation continuously.
    private IEnumerator PlayIdleAnimation()
    {
        Sprite[] frames = currentPatron.idleFrames;

        float frameRate = Mathf.Max(1f, currentPatron.idleFrameRate);
        float frameDuration = 1f / frameRate;

        int currentFrame = 0;

        while (true)
        {
            if (patronImage != null)
                patronImage.sprite = frames[currentFrame];

            yield return new WaitForSecondsRealtime(frameDuration);

            currentFrame++;

            // Return to the first frame when the animation ends.
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }
        }
    }


    // Plays the talking animation once.
    private IEnumerator PlayTalkingAnimation()
    {
        Sprite[] frames = currentPatron.talkingFrames;

        float frameRate = Mathf.Max(1f, currentPatron.talkingFrameRate);
        float frameDuration = 1f / frameRate;

        // Play each talking frame in order.
        for (int i = 0; i < frames.Length; i++)
        {
            if (patronImage != null)
                patronImage.sprite = frames[i];

            yield return new WaitForSecondsRealtime(frameDuration);
        }

        // Talking has finished.
        // Return to the looping idle animation.
        animationCoroutine = null;

        PlayIdle();
    }
}