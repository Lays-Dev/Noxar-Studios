
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotMovement : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [Header("Pot Temperature")]
    public float potValue = 50f;
    public float valueChangeRate = 5f;

    [Header("Audio")]
    [SerializeField] private AudioSource moveAudio;
    [SerializeField] private AudioSource temperatureAudio;
    [SerializeField] private AudioClip heatingSFX;
    [SerializeField] private AudioClip coolingSFX;

    // Tracks whether the pot is currently heating.
    private bool wasHeating = false;
    private bool temperatureStateInitialized = false;

    [Header("Pot UI Bar")]
    public Image potBarFill;

    [Header("Game Panels")]
    public GameObject grillingPanel;
    public GameObject loseScreen;
    public GameObject winScreen;

    private Burner currentBurner;

    [SerializeField] private bool gameLost = false;
    [SerializeField] private bool gameEnded = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (gameLost || gameEnded)
            return;

        // Check whether the pot should heat up.
        bool isHeating =
            currentBurner != null &&
            currentBurner.state == BurnerState.Hot;

        // Play sound only when heating/cooling state changes.
        if (!temperatureStateInitialized)
        {
            wasHeating = isHeating;
            temperatureStateInitialized = true;
        }
        else if (isHeating != wasHeating)
        {
            if (temperatureAudio != null)
            {
                AudioClip sound = isHeating ? heatingSFX : coolingSFX;

                if (sound != null)
                {
                    temperatureAudio.PlayOneShot(sound);
                }
            }

            wasHeating = isHeating;
        }

        // Change pot temperature.
        if (isHeating)
        {
            potValue += valueChangeRate * Time.deltaTime;
        }
        else
        {
            potValue -= valueChangeRate * Time.deltaTime;
        }

        potValue = Mathf.Clamp(potValue, 0f, 100f);

        // Update temperature bar.
        if (potBarFill != null)
        {
            potBarFill.fillAmount = potValue / 100f;
        }

        // Check for losing.
        if (potValue <= 0f)
        {
            LoseGame();
        }
        // Check for winning.
        else if (potValue >= 100f)
        {
            WinGame();
        }
    }

    private void LoseGame()
    {
        gameLost = true;

        if (grillingPanel != null)
            grillingPanel.SetActive(false);

        if (loseScreen != null)
            loseScreen.SetActive(true);
    }

    private void WinGame()
    {
        gameEnded = true;

        if (grillingPanel != null)
            grillingPanel.SetActive(false);

        if (winScreen != null)
            winScreen.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;

        currentBurner = null;

        if (moveAudio != null)
            moveAudio.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject droppedObject =
            eventData.pointerCurrentRaycast.gameObject;

        Burner target = null;

        if (droppedObject != null)
        {
            target = droppedObject.GetComponentInParent<Burner>();
        }

        canvasGroup.blocksRaycasts = true;

        if (moveAudio != null)
            moveAudio.Stop();

        if (target != null)
        {
            currentBurner = target;

            Debug.Log("Pot placed on burner: " + target.state);
        }
        else
        {
            currentBurner = null;

            Debug.Log("The pot is not on a burner.");
        }
    }
}
