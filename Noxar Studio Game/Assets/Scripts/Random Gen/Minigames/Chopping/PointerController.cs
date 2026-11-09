using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class PointerController : MonoBehaviour
{
    [Header("Pointer Movement")]
    public Transform pointA;
    public Transform pointB;
    public RectTransform safeZone;
    public float moveSpeed = 400f;

    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    [Header("Chopping Settings")]
    public int successfulChops = 0;
    public int chopsRequired = 3;

    private bool gameWon = false;

    [Header("Ingredient Display")]
    public Image ingredientImage;
    private bool firstAttemptMade = false;

    private DishData currentDish;
    private int currentIngredientIndex = 0;

    // Stores the ingredients that need to be chopped
    private List<IngredientData> choppingQueue =
        new List<IngredientData>();

    // Tracks the original number of ingredients
    private int originalIngredientCount;

    // Tracks whether the current ingredient is an extra
    private bool currentIngredientIsExtra = false;

    [Header("Extra Ingredients")]
    public int extraIngredientsAdded = 0;

    [Header("Win Screen")]
public GameObject winBackground;

[Header("Chopping Input")]
public bool canChop = false;


    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();

        targetPosition = pointB.position;

        // Get the currently selected dish
        currentDish = LevelManager.Instance.currentDish;

        if (currentDish == null)
        {
            Debug.LogError("No dish has been selected!");
            enabled = false;
            return;
        }

        // Check that the dish has ingredients
        if (currentDish.ingredients == null ||
            currentDish.ingredients.Count == 0)
        {
            Debug.LogError("This dish has no ingredients!");
            enabled = false;
            return;
        }

        // Copy the dish ingredients into the chopping queue
        choppingQueue.AddRange(currentDish.ingredients);

        // Remember how many ingredients the dish originally had
        originalIngredientCount = choppingQueue.Count;

        // Display the first ingredient
        DisplayIngredient();

        Debug.Log("Ingredients to chop: " + choppingQueue.Count);
    }


void Update()
{
    // Don't process anything if the game has been won
    if (gameWon)
        return;

    // Keep moving the pointer
    pointerTransform.position = Vector3.MoveTowards(
        pointerTransform.position,
        targetPosition,
        moveSpeed * Time.deltaTime
    );

    // Change direction at endpoints
    if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
    {
        targetPosition = pointB.position;
    }
    else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
    {
        targetPosition = pointA.position;
    }

    // Don't allow chopping until instructions are closed
    if (!canChop)
        return;

    // Mobile touch input
    if (Touchscreen.current != null &&
        Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
    {
        CheckSuccess();
    }
}


    void CheckSuccess()
    {
        if (gameWon)
            return;

        // Check if pointer is inside safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(
            safeZone,
            pointerTransform.position,
            null))
        {
            successfulChops++;

            Debug.Log("Successful chops: " + successfulChops);

            // First attempt was successful
            if (!firstAttemptMade)
            {
                IngredientData ingredient =
                    choppingQueue[currentIngredientIndex];

                SetIngredientSprite(ingredient.cutSprite);

                firstAttemptMade = true;

                Debug.Log(ingredient.ingredientName + " was cut successfully!");
            }

            // Check if ingredient has been chopped enough
            if (successfulChops >= chopsRequired)
            {
                NextIngredient();
            }
        }
        else
        {
            Debug.Log("Fail!");

            // Only process the first failed attempt
            if (!firstAttemptMade)
            {
                IngredientData ingredient =
                    choppingQueue[currentIngredientIndex];

                // Display badly cut sprite
                SetIngredientSprite(ingredient.badlyCutSprite);

                firstAttemptMade = true;

                Debug.Log(ingredient.ingredientName + " was badly cut!");

                // ALWAYS add the badly chopped ingredient to the end
                // of the queue, even if it is already an extra.
                choppingQueue.Add(ingredient);

                extraIngredientsAdded++;

                Debug.Log("Extra ingredient added: " +
                    ingredient.ingredientName);

                Debug.Log("Total ingredients to chop: " +
                    choppingQueue.Count);
            }
        }

        // Randomize speed after every attempt
        RandomizeSpeed();
    }


    void NextIngredient()
    {
        currentIngredientIndex++;

        // Check if all ingredients, including extras, are finished
        if (currentIngredientIndex >= choppingQueue.Count)
        {
            WinGame();
            return;
        }

        // Reset chop counter
        successfulChops = 0;

        // Reset first attempt
        firstAttemptMade = false;

        // Check if the next ingredient is an extra
        currentIngredientIsExtra =
            currentIngredientIndex >= originalIngredientCount;

        // Display next ingredient
        DisplayIngredient();
    }


    void DisplayIngredient()
    {
        if (currentIngredientIndex >= choppingQueue.Count)
            return;

        IngredientData ingredient =
            choppingQueue[currentIngredientIndex];

        // Display uncut sprite
        SetIngredientSprite(ingredient.uncutSprite);

        Debug.Log("Now chopping: " + ingredient.ingredientName);

        if (currentIngredientIsExtra)
        {
            Debug.Log("This is an extra ingredient!");
        }
    }


void WinGame()
{
    gameWon = true;

    Debug.Log("All ingredients chopped! Chopping Game Won!");

    Debug.Log("Extra ingredients added: " + extraIngredientsAdded);

    // Activate the win background
    winBackground.SetActive(true);
}


    void RandomizeSpeed()
    {
        int[] possibleSpeeds = { 400, 500, 600, 700 };

        // Randomly select one of the four speeds
        moveSpeed = possibleSpeeds[
            Random.Range(0, possibleSpeeds.Length)
        ];

        Debug.Log("New pointer speed: " + moveSpeed);
    }
    void SetIngredientSprite(Sprite newSprite)
{
    if (newSprite == null)
        return;

    // Remember the current UI Image dimensions.
    RectTransform imageRect = ingredientImage.rectTransform;

    Vector2 originalSize = imageRect.sizeDelta;

    // Change the sprite.
    ingredientImage.sprite = newSprite;

    // Restore the original UI Image dimensions.
    imageRect.sizeDelta = originalSize;

    // Maintain the sprite's aspect ratio within the existing dimensions.
    ingredientImage.preserveAspect = true;
}
}
