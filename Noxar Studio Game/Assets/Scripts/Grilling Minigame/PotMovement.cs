using UnityEngine;
using UnityEngine.EventSystems;

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

    // The burner the pot is currently on, if any
    private Burner currentBurner;

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
        // Check the burner every frame
        if (currentBurner != null)
        {
            if (currentBurner.state == BurnerState.Hot)
            {
                // Increase the pot value on a hot burner
                potValue += valueChangeRate * Time.deltaTime;
            }
            else
            {
                // Decrease the pot value on a cold burner
                potValue -= valueChangeRate * Time.deltaTime;
            }

            Debug.Log("Pot Value: " + potValue);
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
