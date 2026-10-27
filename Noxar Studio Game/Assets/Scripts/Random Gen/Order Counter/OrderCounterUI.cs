
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    private LevelManager levelManager;
    private bool orderDisplayed = false;

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
            // Assign the randomly selected patron sprite.
            patronImage.sprite = levelManager.currentPatron.patronSprite;

            // Set the UI Image to the sprite's native dimensions.
            patronImage.SetNativeSize();

            // Keep the sprite's original proportions.
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

        orderDisplayed = true;

        // Show the dialogue panel with the randomly selected dish.
        if (dialogueDishImage != null)
        {
            dialogueDishImage.sprite = levelManager.currentDish.dishSprite;
            dialogueDishImage.preserveAspect = true;
        }

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Show the order ticket and its minigame icons.
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

        // Clear any old icons first.
        for (int i = minigameIconsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(minigameIconsContainer.GetChild(i).gameObject);
        }

        // Add an icon for each randomly selected minigame.
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
        if (!levelManager.orderTaken)
        {
            Debug.Log("Take the patron's order first!");
            return;
        }

        SceneManager.LoadScene("Kitchen");
    }
}
