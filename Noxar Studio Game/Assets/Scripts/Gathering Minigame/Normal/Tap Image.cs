using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TapImage : MonoBehaviour
{
    [Header("Ingredient Identification")]
    public string ingredientName;

    [Header("Ingredient References")]
    public GameObject nextIngredient;
    public GameObject armParent;
    public GameObject fridgeOpen;

    [Header("Ingredient Sound")]
    public AudioSource tapSFX;

    [Header("Ingredient UI")]
    public Image ingredientImage;

    private RectTransform image;
    private int tapCount = 0;

    private bool ingredientFinished = false;

    private GatheringMinigameManager gatheringManager;
    public class UITAPVFXSPAWNER : MonoBehaviour
    {
        [Header("Ui VFX")]
        public GameObject vfxPrefab;
        public Canvas targetCanvas;//see if new name needs to be done

        [Header("Animation")]
        public float effectLifeTime = 1f;

        private RectTransform CanvasRect;
        private Camera uiCamera;

        private void Awake()
        {
            if (targetCanvas == null)
                targetCanvas = GetComponentInParent<Canvas>();

            if (targetCanvas != null)
            {
                CanvasRect = targetCanvas.GetComponent< RectTransform>();
                if (targetCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
                    uiCamera = targetCanvas.worldCamera;
            }
        }
  
    }
  



    private void Start()
    {
        image = GetComponent<RectTransform>();
    }

    // Called by GatheringMinigameManager.
    // Assigns this ingredient's information from the selected dish.
    public void SetupIngredient(
        IngredientData ingredient,
        GameObject next,
        bool isLast,
        GatheringMinigameManager manager)
    {
        ingredientName = ingredient.ingredientName;

        nextIngredient = next;
        gatheringManager = manager;

        tapCount = 0;
        ingredientFinished = false;

        // Display the uncut ingredient sprite.
        Image ingredientImage = GetComponent<UnityEngine.UI.Image>();

        if (ingredientImage != null)
        {
            ingredientImage.sprite = ingredient.uncutSprite;
            ingredientImage.preserveAspect = true;
        }

        // Only the first ingredient should be active initially.
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (ingredientFinished)
            return;

        // Check for mobile touch.
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
            Vector2 screenPosition;

            Canvas canvas = GetComponentInParent<Canvas>();
            Camera cam = null;

            if (canvas != null &&
                canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                cam = canvas.worldCamera;
            }

            // Check whether this ingredient was tapped.
            if (RectTransformUtility.RectangleContainsScreenPoint(
                image, touchPosition, cam))
            {
                tapCount++;

                Debug.Log(ingredientName + " taps: " + tapCount);

                if (tapSFX != null)
                    tapSFX.Play();

                // Require two taps before moving to the next ingredient.
                if (tapCount >= 2)
                {
                    FinishIngredient();
                }

                if(Touchscreen.current !=null &&
                    Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                {
                    screenPosition = 
                        Touchscreen.current.primaryTouch.position.ReadValue();
                }
                else if (Mouse.current != null &&
                          Mouse.current.leftButton.wasPressedThisFrame)
                {
                    screenPosition = Mouse.current.position.ReadValue();
                }
                else
                {
                    return;
                }

            }
            
        }
    }

    
private void FinishIngredient()
{
    ingredientFinished = true;

    Debug.Log(ingredientName + " gathered!");

    // Hide the current arm.
    if (armParent != null)
    {
        armParent.SetActive(false);
    }

    // Check whether another ingredient needs to appear.
    if (nextIngredient != null)
    {
        // Activate the next ingredient's parent Arm first.
        Transform nextParent = nextIngredient.transform.parent;

        if (nextParent != null)
        {
            nextParent.gameObject.SetActive(true);

            Debug.Log("Activated next ingredient parent: " +
                nextParent.name);
        }

        // Activate the next ingredient.
        nextIngredient.SetActive(true);

        Debug.Log("Activated next ingredient: " +
            nextIngredient.name);

        Debug.Log("Next ingredient active in hierarchy: " +
            nextIngredient.activeInHierarchy);
    }
    else
    {
        // There are no more ingredients.
        Debug.Log("All ingredients gathered!");

        if (fridgeOpen != null)
        {
            fridgeOpen.SetActive(false);
        }

        if (gatheringManager != null)
        {
            gatheringManager.FinishGathering();
        }
    }

    // Prevent this ingredient from being tapped again.
    enabled = false;
}

}
