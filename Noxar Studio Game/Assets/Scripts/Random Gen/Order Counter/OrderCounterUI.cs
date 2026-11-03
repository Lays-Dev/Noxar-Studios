
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

    // NEW: Kitchen button SFX
    [Header("Kitchen Button SFX")]
    public AudioSource audioSource;
    public AudioClip kitchenButtonSFX;

    
    [Header("Animated Dish VFX")]
    public Image dialogueVFXImage;
    public Image dishVFXImage;

    private Coroutine vfxAnimationCoroutine;

    [Header("Patron Animation")]
    public Animator patronAnimator;

    private RuntimeAnimatorController originalController;
    public AnimatorOverrideController patronOverrideController;



    private LevelManager levelManager;
    private bool orderDisplayed = false;

    // NEW: Prevent repeated clicks while the sound plays.
    private bool isLoadingKitchen = false;

    private void Start()
    {
        levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError("OrderCounterUI: LevelManager was not found!");
            return;
        }

        if (levelManager.currentPatron == null ||
            levelManager.currentDish == null ||
            levelManager.requiredMinigames.Count == 0)
        {
            levelManager.GenerateLevel();
        }

        if (patronButton != null)
        {
            patronButton.onClick.RemoveListener(OnPatronTapped);
            patronButton.onClick.AddListener(OnPatronTapped);
        }

        if (goToKitchenButton != null)
        {
            goToKitchenButton.onClick.RemoveListener(GoToKitchen);
            goToKitchenButton.onClick.AddListener(GoToKitchen);
        }

        DisplayPatron();
        SetupPatronAnimation();

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (orderPanel != null)
            orderPanel.SetActive(false);

        if (goToKitchenButton != null)
            goToKitchenButton.interactable = false;

        if (levelManager.orderTaken)
        {
            DisplayOrder();
        }
    }

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
    }

    public void OnPatronTapped()
    {
        if (orderDisplayed || levelManager.orderTaken)
            return;

        levelManager.TakeOrder();

        if (!levelManager.orderTaken)
            return;
        
        if (patronAnimator != null)
        {
            patronAnimator.SetTrigger("Talk");
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

        if (dialogueDishImage != null)
        {
            dialogueDishImage.sprite = levelManager.currentDish.dishSprite;
            dialogueDishImage.preserveAspect = true;
        }
        DisplayDishVFX();

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        DisplayOrder();

        if (patronButton != null)
            patronButton.interactable = false;

        if (goToKitchenButton != null)
            goToKitchenButton.interactable = true;
    }

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

        for (int i = minigameIconsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(minigameIconsContainer.GetChild(i).gameObject);
        }

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

    public void GoToKitchen()
    {
        if (isLoadingKitchen)
            return;

        if (!levelManager.orderTaken)
        {
            Debug.Log("Take the patron's order first!");
            return;
        }

        // NEW: Play the sound before loading the Kitchen scene.
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

    // NEW: Wait for the sound to finish before changing scenes.
    private IEnumerator PlaySoundThenLoadKitchen()
    {
        isLoadingKitchen = true;

        if (goToKitchenButton != null)
            goToKitchenButton.interactable = false;

        audioSource.PlayOneShot(kitchenButtonSFX);

        yield return new WaitForSecondsRealtime(kitchenButtonSFX.length);

        SceneManager.LoadScene("Kitchen");
    }

    

private void DisplayDishVFX()
{
    // Stop the previous dish's animation.
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

// Plays the animated VFX.
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
                    yield break;
                }
            }

            SetVFXSprite(frames[currentFrame]);
        }

        vfxAnimationCoroutine = null;
        yield return null;
    }
}

    private void OnDisable()
    {
        if (vfxAnimationCoroutine != null)
        {
            StopCoroutine(vfxAnimationCoroutine);
            vfxAnimationCoroutine = null;
        }
    }

    private void SetupPatronAnimation()
    {
        if (patronAnimator == null)
            return;

        PatronData patron = levelManager.currentPatron;

        if (patron == null)
            return;

        if (patronOverrideController == null)
            return;

        // Create a separate override for this patron.
        AnimatorOverrideController newOverride =
            new AnimatorOverrideController(patronOverrideController);

        // Replace the animation clips.
        newOverride["Idle"] = patron.idleAnimation;
        newOverride["Talking"] = patron.talkingAnimation;

        // Apply the animations.
        patronAnimator.runtimeAnimatorController = newOverride;

        // Start idle animation.
        patronAnimator.Play("Idle", 0, 0f);
    }

}

