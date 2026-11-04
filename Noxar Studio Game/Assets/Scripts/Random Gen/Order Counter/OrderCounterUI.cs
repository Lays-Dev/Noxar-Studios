
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class OrderCounterUI : MonoBehaviour
{
    [Header("Patron")]
    public Button patronButton;
    public Image patronImage;
    public TMP_Text patronNameText;

    [Header("Patron Animation")]
    public PatronUIAnimator patronUIAnimator;

    [Header("Dialogue Panel")]
    public GameObject dialoguePanel;
    public Image dialogueDishImage;

    [Header("Order Ticket")]
    public GameObject orderPanel;
    public Image dishImage;
    public TMP_Text dishNameText;

    [Header("Minigame Icons")]
    public Transform minigameIconsContainer;
    public GameObject minigameOrderItemPrefab;

    [Header("Navigation")]
    public Button goToKitchenButton;

    [Header("Kitchen Button SFX")]
    public AudioSource audioSource;
    public AudioClip kitchenButtonSFX;

    [Header("Animated Dish VFX")]
    public Image dialogueVFXImage;
    public Image dishVFXImage;

    private Coroutine vfxAnimationCoroutine;

    private LevelManager levelManager;

    private bool orderDisplayed = false;
    private bool isLoadingKitchen = false;


    private void Start()
    {
        // Find the LevelManager.
        levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError("OrderCounterUI: LevelManager was not found!");
            return;
        }

        // Generate a level if one has not already been generated.
        if (levelManager.currentPatron == null ||
            levelManager.currentDish == null ||
            levelManager.requiredMinigames.Count == 0)
        {
            levelManager.GenerateLevel();
        }

        // Connect the patron button.
        if (patronButton != null)
        {
            patronButton.onClick.RemoveListener(OnPatronTapped);
            patronButton.onClick.AddListener(OnPatronTapped);
        }

        // Connect the kitchen button.
        if (goToKitchenButton != null)
        {
            goToKitchenButton.onClick.RemoveListener(GoToKitchen);
            goToKitchenButton.onClick.AddListener(GoToKitchen);
        }

        // Display the randomly selected patron.
        DisplayPatron();

        // Hide the dialogue panel at the beginning.
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Hide the order ticket at the beginning.
        if (orderPanel != null)
            orderPanel.SetActive(false);

        // The player cannot enter the kitchen until the order is taken.
        if (goToKitchenButton != null)
            goToKitchenButton.interactable = false;

        // If the order was already taken, display it.
        if (levelManager.orderTaken)
        {
            DisplayOrder();
        }
    }


    // Displays the selected patron and starts their idle animation.
    private void DisplayPatron()
    {
        if (levelManager.currentPatron == null)
            return;

        if (patronImage != null)
        {
            patronImage.sprite = levelManager.currentPatron.patronSprite;
            patronImage.SetNativeSize();
            patronImage.preserveAspect = true;
        }

        if (patronNameText != null)
        {
            patronNameText.text = levelManager.currentPatron.patronName;
        }

        // Give the selected patron to the animation script.
        // This starts that patron's unique idle animation.
        if (patronUIAnimator != null)
        {
            patronUIAnimator.SetPatron(levelManager.currentPatron);
        }
        else
        {
            Debug.LogWarning("OrderCounterUI: Patron UI Animator is not assigned!");
        }
    }


    // Called when the player taps the patron.
    public void OnPatronTapped()
    {
        // Prevent the player from taking the same order twice.
        if (orderDisplayed || levelManager.orderTaken)
            return;

        // Tell the LevelManager that the order has been taken.
        levelManager.TakeOrder();

        if (!levelManager.orderTaken)
            return;

        // Play the patron's unique talking animation once.
        if (patronUIAnimator != null)
        {
            patronUIAnimator.SetTalking();
        }

        // Play the sound assigned to the randomly selected patron.
        if (audioSource != null &&
            levelManager.currentPatron != null &&
            levelManager.currentPatron.patronClickSFX != null)
        {
            audioSource.PlayOneShot(
                levelManager.currentPatron.patronClickSFX
            );
        }

        orderDisplayed = true;

        // Display the dish image in the dialogue panel.
        if (dialogueDishImage != null)
        {
            dialogueDishImage.sprite = levelManager.currentDish.dishSprite;
            dialogueDishImage.preserveAspect = true;
        }

        // Play the dish VFX.
        DisplayDishVFX();

        // Show the dialogue panel.
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Display the order ticket.
        DisplayOrder();

        // Prevent the patron from being tapped again.
        if (patronButton != null)
            patronButton.interactable = false;

        // Allow the player to enter the kitchen.
        if (goToKitchenButton != null)
            goToKitchenButton.interactable = true;
    }


    // Displays the order ticket.
    private void DisplayOrder()
    {
        if (levelManager.currentDish == null)
            return;

        orderDisplayed = true;

        if (orderPanel != null)
            orderPanel.SetActive(true);

        if (dishImage != null)
        {
            dishImage.sprite = levelManager.currentDish.dishSprite;
            dishImage.preserveAspect = true;
        }

        if (dishNameText != null)
        {
            dishNameText.text = levelManager.currentDish.dishName;
        }

        DisplayDishVFX();
        DisplayMinigameIcons();

        if (patronButton != null)
            patronButton.interactable = false;

        if (goToKitchenButton != null)
            goToKitchenButton.interactable = true;
    }


    // Creates the required minigame icons on the order ticket.
    private void DisplayMinigameIcons()
    {
        if (minigameIconsContainer == null)
        {
            Debug.LogWarning("Assign the Minigame Icons Container.");
            return;
        }

        if (minigameOrderItemPrefab == null)
        {
            Debug.LogWarning("Assign the Minigame Order Item Prefab.");
            return;
        }

        // Remove any previously created icons.
        for (int i = minigameIconsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(minigameIconsContainer.GetChild(i).gameObject);
        }

        // Create an icon for each required minigame.
        foreach (MinigameData minigame in levelManager.requiredMinigames)
        {
            if (minigame == null)
                continue;

            GameObject item = Instantiate(
                minigameOrderItemPrefab,
                minigameIconsContainer,
                false
            );

            MinigameOrderItemUI itemUI =
                item.GetComponent<MinigameOrderItemUI>();

            if (itemUI != null)
            {
                itemUI.Setup(minigame);
            }
            else
            {
                Debug.LogError(
                    "MinigameOrderItem prefab needs MinigameOrderItemUI."
                );
            }
        }
    }


    // Called by the kitchen button.
    public void GoToKitchen()
    {
        if (isLoadingKitchen)
            return;

        if (!levelManager.orderTaken)
        {
            Debug.Log("Take the patron's order first!");
            return;
        }

        // Play the sound before loading the Kitchen scene.
        if (audioSource != null && kitchenButtonSFX != null)
        {
            StartCoroutine(PlaySoundThenLoadKitchen());
        }
        else
        {
            Debug.LogWarning(
                "Kitchen button SFX or AudioSource is missing. Loading Kitchen without sound."
            );

            SceneManager.LoadScene("Kitchen");
        }
    }


    // Waits for the kitchen button sound to finish before changing scenes.
    private IEnumerator PlaySoundThenLoadKitchen()
    {
        isLoadingKitchen = true;

        if (goToKitchenButton != null)
            goToKitchenButton.interactable = false;

        audioSource.PlayOneShot(kitchenButtonSFX);

        yield return new WaitForSecondsRealtime(kitchenButtonSFX.length);

        SceneManager.LoadScene("Kitchen");
    }


    // Starts the animated dish VFX.
    private void DisplayDishVFX()
    {
        // Stop the previous dish VFX animation.
        if (vfxAnimationCoroutine != null)
        {
            StopCoroutine(vfxAnimationCoroutine);
            vfxAnimationCoroutine = null;
        }

        // Get the currently selected dish.
        DishData dish = levelManager.currentDish;

        if (dish == null)
            return;

        // Get the animation frames.
        Sprite[] frames = dish.dishVFXFrames;

        // Check whether animation frames exist.
        if (frames == null || frames.Length == 0)
        {
            if (dialogueVFXImage != null)
                dialogueVFXImage.gameObject.SetActive(false);

            if (dishVFXImage != null)
                dishVFXImage.gameObject.SetActive(false);

            return;
        }

        // Show both VFX Images.
        if (dialogueVFXImage != null)
            dialogueVFXImage.gameObject.SetActive(true);

        if (dishVFXImage != null)
            dishVFXImage.gameObject.SetActive(true);

        // Display the first animation frame immediately.
        SetVFXSprite(frames[0]);

        // Start the animation.
        vfxAnimationCoroutine = StartCoroutine(
            PlayDishVFX(dish)
        );
    }


    // Updates both UI Images with the same sprite.
    private void SetVFXSprite(Sprite sprite)
    {
        if (dialogueVFXImage != null)
        {
            dialogueVFXImage.sprite = sprite;
            dialogueVFXImage.preserveAspect = true;
        }

        if (dishVFXImage != null)
        {
            dishVFXImage.sprite = sprite;
            dishVFXImage.preserveAspect = true;
        }
    }


    // Plays the animated dish VFX.
    private IEnumerator PlayDishVFX(DishData dish)
    {
        Sprite[] frames = dish.dishVFXFrames;

        float frameRate = Mathf.Max(1f, dish.vfxFramesPerSecond);
        float frameDuration = 1f / frameRate;

        int currentFrame = 0;
        float timer = 0f;

        while (true)
        {
            timer += Time.unscaledDeltaTime;

            if (timer >= frameDuration)
            {
                timer -= frameDuration;

                currentFrame++;

                // Check whether the animation has reached its end.
                if (currentFrame >= frames.Length)
                {
                    if (dish.loopVFX)
                    {
                        currentFrame = 0;
                    }
                    else
                    {
                        // Keep the final frame visible.
                        currentFrame = frames.Length - 1;
                        SetVFXSprite(frames[currentFrame]);

                        vfxAnimationCoroutine = null;
                        yield break;
                    }
                }

                SetVFXSprite(frames[currentFrame]);
            }

            yield return null;
        }
    }


    private void OnDisable()
    {
        // Stop the dish VFX animation if this object is disabled.
        if (vfxAnimationCoroutine != null)
        {
            StopCoroutine(vfxAnimationCoroutine);
            vfxAnimationCoroutine = null;
        }
    }
}

