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

    private void Awake()
    {
        // Get the UI image's position and size
        rectTransform = GetComponent<RectTransform>();

        // Find the Canvas containing this image
        canvas = GetComponentInParent<Canvas>();

        // Get the CanvasGroup used for raycast detection
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Called when the player starts dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Let the finger detect UI elements behind this image
        canvasGroup.blocksRaycasts = false;
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

        // Check whether the player dropped on a target
        if (target != null)
        {
            target.AcceptDrop();
        }
        else
        {
            Debug.Log("The pot was not placed on a burner.");
        }
    }
}
