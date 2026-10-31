using Unity.VisualScripting;
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

    [Header("Audio")]
    [SerializeField] private AudioSource moveAudio;

    [Header("Pot UI Bar")]
    public Image potBarFill;

    [Header("Game Panels")]
    public GameObject grillingPanel;
    public GameObject loseScreen;
    public GameObject winScreen;

    //Adding Pot Images Mahdeyah Added this
    [Header("Pot Images")]
    public Image PotImage;
    public Sprite PotArtHot;
    public Sprite PotArt;

  
    // The burner the pot is currently on, if any
    private Burner currentBurner;
    [SerializeField] private bool gameLost = false;
    [SerializeField] private bool gameEnded = false;
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
        if (currentBurner != null && currentBurner.state == BurnerState.Hot)
        {
            // The pot heats up on a hot burner.
            potValue += valueChangeRate * Time.deltaTime;
        }
        else
        {
            // The pot cools down on a cold burner or when not on any burner.
            potValue -= valueChangeRate * Time.deltaTime;
        }

        // Keep the pot value between 0 and 100
        potValue = Mathf.Clamp(potValue, 0f, 100f);

        //Adding Hot and cold Temp Mahdeyah Added this

        if (potValue >= 50f)
        {
           PotImage.sprite = PotArtHot;
        }
        else
        {
            PotImage.sprite = PotArt;
        }
    
        //Insert Hot and cold Pot image components
       
        // Update the UI bar
        if (potBarFill != null)
        {
            potBarFill.fillAmount = potValue / 100f;
        }

        // Check for losing
        if (potValue <= 0f)
        {
            LoseGame();
        }
        // Check for winning
        else if (potValue >= 100f)
        {
            WinGame();
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

    private void WinGame()
    {
        gameEnded = true;

        if (grillingPanel != null)
            grillingPanel.SetActive(false);

        if (winScreen != null)
            winScreen.SetActive(true);
    }

    // Called when the player starts dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Let the finger detect UI elements behind this image
        canvasGroup.blocksRaycasts = false;

        // Stop checking the previous burner while dragging
        currentBurner = null;

        if (moveAudio !=null)
            moveAudio.Play();
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

        if (moveAudio != null)
        moveAudio.Stop();


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
