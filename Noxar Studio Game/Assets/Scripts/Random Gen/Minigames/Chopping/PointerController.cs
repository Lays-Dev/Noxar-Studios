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

    [Header("SFX")]
    public AudioSource audioSource;
    public AudioClip chopSound;
    public AudioClip failSound;

    private bool gameWon = false;

    [Header("Ingredient Display")]
    public Image ingredientImage;
    private bool firstAttemptMade = false;
    private bool ingredientCut = false;

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
        Debug.Log("CHOPS REQUIRED: " + chopsRequired);
        if (gameWon)
            return;

        IngredientData ingredient =
            choppingQueue[currentIngredientIndex];

        // Check if pointer is inside safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(
            safeZone,
            pointerTransform.position,
            null))
        {
            if (audioSource != null && chopSound != null)
            {
                audioSource.PlayOneShot(chopSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or chopSound is not assigned!");
            }

            successfulChops++;

            Debug.Log("Successful chops: " + successfulChops);

            // FIRST successful chop: display cut sprite
            if (!ingredientCut)
            {
                if (ingredient.cutSprite != null)
                {
                    SetIngredientSprite(ingredient.cutSprite);
                    Debug.Log("Cut sprite name: " + ingredient.cutSprite.name);
                    Debug.Log("Currently displayed sprite: " + ingredientImage.sprite.name);
                    Debug.Log("Successful chops: " + successfulChops);

                    Debug.Log("CUT SPRITE DISPLAYED: " +
                        ingredient.ingredientName);
                }
                else
                {
                    Debug.LogError("No cut sprite assigned to: " +
                        ingredient.ingredientName);
                }

                ingredientCut = true;
            }

            // SECOND successful chop: move to next ingredient
            if (successfulChops == chopsRequired)
            {
                NextIngredient();
            }
        }
        else
        {
            if (audioSource != null && failSound != null)
            {
                audioSource.PlayOneShot(failSound);
            }
            else
            {
                Debug.LogWarning("AudioSource or failSound is not assigned!");
            }
            Debug.Log("Fail!");

            // Only process the first failed attempt
            if (!firstAttemptMade)
            {
                if (ingredient.badlyCutSprite != null)
                {
                    SetIngredientSprite(ingredient.badlyCutSprite);
                }

                firstAttemptMade = true;

                Debug.Log(ingredient.ingredientName +
                    " was badly cut!");

                // Add another copy to the end of the queue
                choppingQueue.Add(ingredient);

                extraIngredientsAdded++;

                Debug.Log("Extra ingredient added: " +
                    ingredient.ingredientName);
            }
        }

        RandomizeSpeed();
    }


    void NextIngredient()
    {
        currentIngredientIndex++;

        if (currentIngredientIndex >= choppingQueue.Count)
        {
            WinGame();
            return;
        }

        successfulChops = 0;

        firstAttemptMade = false;

        ingredientCut = false;

        currentIngredientIsExtra =
            currentIngredientIndex >= originalIngredientCount;

        DisplayIngredient();
    }


    void DisplayIngredient()
    {
        if (currentIngredientIndex >= choppingQueue.Count)
            return;

        IngredientData ingredient =
            choppingQueue[currentIngredientIndex];

        Debug.Log("Now displaying ingredient: " +
            ingredient.ingredientName);

        if (ingredient.uncutSprite == null)
        {
            Debug.LogError("Missing uncutSprite for: " +
                ingredient.ingredientName);

            return;
        }

        SetIngredientSprite(ingredient.uncutSprite);

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
        {
            Debug.LogError("Trying to display a NULL sprite!");
            return;
        }

        ingredientImage.sprite = newSprite;
        ingredientImage.preserveAspect = true;

        Debug.Log("Image changed to: " + newSprite.name);
    }
}
