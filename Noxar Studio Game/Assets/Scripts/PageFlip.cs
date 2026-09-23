using System.Collections;
using UnityEngine;

/// <summary>
/// Flips the current page and reveals the next page.
/// The page's RectTransform pivot should be set to (0, 0.5)
/// so it rotates around its left edge like a spine.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class PageFlip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject frontSide;   // buttons/art shown before flip
    [SerializeField] private GameObject backSide;    // art shown after flip
    [SerializeField] private GameObject nextPage;    // next page to reveal

    [Header("Timing")]
    [SerializeField] private float flipDuration = 0.5f;
    [SerializeField] private AnimationCurve ease =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private RectTransform rect;
    private bool isFlipping;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Hook this up to the button's OnClick()
    public void FlipNext()
    {
        if (!isFlipping)
            StartCoroutine(FlipRoutine());
    }

    private IEnumerator FlipRoutine()
    {
        isFlipping = true;

        float t = 0f;
        bool swapped = false;

        while (t < flipDuration)
        {
            t += Time.deltaTime;

            float progress = ease.Evaluate(t / flipDuration);
            float angle = Mathf.Lerp(0f, 180f, progress);

            rect.localEulerAngles = new Vector3(0f, angle, 0f);

            // Change the visible side halfway through the flip
            if (!swapped && angle >= 90f)
            {
                if (frontSide)
                    frontSide.SetActive(false);

                if (backSide)
                    backSide.SetActive(true);

                swapped = true;
            }

            yield return null;
        }

        // Make sure the page finishes at exactly 180 degrees
        rect.localEulerAngles = new Vector3(0f, 180f, 0f);

        // Show the next page
        if (nextPage)
        {
            nextPage.SetActive(true);

            // Reset the next page so its front is active
            PageFlip nextPageFlip = nextPage.GetComponent<PageFlip>();

            if (nextPageFlip != null)
                nextPageFlip.ResetPage();
        }

        // Hide the page that just finished flipping
        gameObject.SetActive(false);

        isFlipping = false;
    }

    // Resets this page when it is revealed again
    public void ResetPage()
    {
        if (frontSide)
            frontSide.SetActive(true);

        if (backSide)
            backSide.SetActive(false);

        rect.localEulerAngles = new Vector3(0f, 0f, 0f);
    }
}