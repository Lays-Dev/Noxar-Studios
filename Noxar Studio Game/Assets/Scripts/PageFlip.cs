using System.Collections;
using UnityEngine;
 
/// <summary>
/// Attach to each "page" GameObject in the right-hand stack.
/// The page's RectTransform pivot MUST be set to (0, 0.5) so it
/// rotates around its left edge like a spine.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class PageFlip : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject frontSide;   // buttons/art shown before flip
    [SerializeField] private GameObject backSide;     // art shown after flip (next page's "underside")
    [SerializeField] private GameObject nextPage;     // page object to reveal/enable once flip completes
 
    [Header("Timing")]
    [SerializeField] private float flipDuration = 0.5f;
    [SerializeField] private AnimationCurve ease =
        AnimationCurve.EaseInOut(0, 0, 1, 1);
 
    private RectTransform rect;
    private bool isFlipping;
 
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
       // rect.pivot = new Vector2(0f, 0.5f); // enforce spine pivot
    }
 
    // Hook this up to the button's OnClick()
    public void FlipNext()
    {
        if (!isFlipping) StartCoroutine(FlipRoutine());
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
 
            // Swap visible face at the halfway point so it "turns over"
            if (!swapped && angle >= 90f)
            {
                if (frontSide) frontSide.SetActive(false);
                if (backSide) backSide.SetActive(true);
                if (nextPage) nextPage.SetActive(true); // reveal page underneath
                swapped = true;
            }
 
            yield return null;
        }
 
        rect.localEulerAngles = new Vector3(0f, 180f, 0f);
        gameObject.SetActive(false); // hide the fully-flipped page
        isFlipping = false;
    }
}