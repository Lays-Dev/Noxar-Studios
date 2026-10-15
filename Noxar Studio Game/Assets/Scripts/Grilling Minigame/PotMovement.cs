using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// IBeginDragHandler detects when the player starts dragging the image
// IDragHandler updates the image's position while dragging
// IEndDragHandler detects when the player releases the image
public class PotMovement : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // Pot's starting temp
    public float potValue = 50f;

    // Temperture change rate per second
    public float valueChangeRate = 5f;

    [Header("Pot UI Bar")]
    public Image potBarFill;

    [Header("Game Panels")]
    public GameObject grillingPanel;
    public GameObject loseScreen;

    // The burner the pot is currently on, if any
    private Burner currentBurner;
    [SerializeField] private bool gameLost = false;
    private void Awake()
    {
        // Get the UI image's position and size
        rectTransform = GetComponent<RectTransform>();

        // Find the Canvas containing this image
        canvas = GetComponentInParent<Canvas>();

        // Get the CanvasGroup used for raycast detection
        canvasGroup = GetComponent<CanvasGroup>();
    }


    private void Update()
    {
        if (gameLost)
            return;

        // Change the pot value based on the burner state
        if (currentBurner != null)
        {
            if (currentBurner.state == BurnerState.Hot)
            {
                potValue += valueChangeRate * Time.deltaTime;
            }
            else
            {
                potValue -= valueChangeRate * Time.deltaTime;
            }
        }

        // Keep the pot value between 0 and 100
        potValue = Mathf.Clamp(potValue, 0f, 100f);

        // Update the UI bar
        if (potBarFill != null)
        {
            potBarFill.fillAmount = potValue / 100f;
        }

        // Check if the pot temperature reaches zero
        if (potValue <= 0f)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        gameLost = true;

        // Hide the grilling panel
        if (grillingPanel != null)
        {
            grillingPanel.SetActive(false);
        }

        // Show the lose screen
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }
    }

    // Called when the player starts dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Let the finger detect UI elements behind this image
        canvasGroup.blocksRaycasts = false;

        // Stop checking the previous burner while dragging
        currentBurner = null;
    }

    // Called while the player drags the image
    public void OnDrag(PointerEventData eventData)
    {
        // Helps the image follow the player correctly on different screen sizes
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    // Called when the player releases the image
    public void OnEndDrag(PointerEventData eventData)
    {
        // Check which UI object is under the finger
        GameObject droppedObject =
            eventData.pointerCurrentRaycast.gameObject;

        // Find a valid drop target
        Burner target = null;

        if (droppedObject != null)
        {
            target = droppedObject.GetComponentInParent<Burner>();
        }

        // Allow the image to receive raycasts again
        canvasGroup.blocksRaycasts = true;

       

        if (target != null)
        {
            // Remember which burner the pot is on
            currentBurner = target;

            Debug.Log("Pot placed on burner: " + target.state);
        }
        else
        {
            // The pot is not on a burner
            currentBurner = null;

            Debug.Log("The pot is not on a burner.");
        }
    }
}
