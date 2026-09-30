using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PageFlip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject frontSide;
    [SerializeField] private GameObject backSide;
    [SerializeField] private GameObject nextPage;

    [Header("Timing")]
    [SerializeField] private float flipDuration = 0.5f;
    [SerializeField] private AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private RectTransform rect;
    private bool isFlipping;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void FlipNext()
    {
        if (isFlipping)
            return;

        FlipRoutine();
    }

    private async void FlipRoutine()
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

            if (!swapped && angle >= 90f)
            {
                if (frontSide)
                    frontSide.SetActive(false);

                if (backSide)
                    backSide.SetActive(true);

                swapped = true;
            }

            await Awaitable.NextFrameAsync();
        }

        rect.localEulerAngles = new Vector3(0f, 180f, 0f);

        // Turn on the other page
        if (nextPage)
        {
            nextPage.SetActive(true);

            PageFlip nextPageFlip = nextPage.GetComponent<PageFlip>();

            if (nextPageFlip != null)
                nextPageFlip.ResetPage();
        }

        isFlipping = false;

        // Turn this page off after the flip
        gameObject.SetActive(false);
    }

    public void ResetPage()
    {
        isFlipping = false;

        rect.localEulerAngles = new Vector3(0f, 0f, 0f);

        if (frontSide)
            frontSide.SetActive(true);

        if (backSide)
            backSide.SetActive(false);
    }
}