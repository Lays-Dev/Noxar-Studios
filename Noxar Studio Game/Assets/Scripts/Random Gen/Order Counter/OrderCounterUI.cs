
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

    [Header("Order Ticket")]
    public GameObject orderPanel;
    public Image dishImage;
    public TMP_Text dishNameText;

    [Header("Navigation")]
    public Button goToKitchenButton;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.Instance;

        if (levelManager == null)
        {
            Debug.LogError("LevelManager was not found!");
            return;
        }

        // Make sure a level exists before displaying the patron.
        if (levelManager.currentPatron == null ||
            levelManager.currentDish == null ||
            levelManager.requiredMinigames.Count == 0)
        {
            levelManager.GenerateLevel();
        }

        // Connect the UI buttons to their functions.
        if (patronButton != null)
            patronButton.onClick.AddListener(OnPatronTapped);

        if (goToKitchenButton != null)
            goToKitchenButton.onClick.AddListener(GoToKitchen);

        // Show the generated patron.
        DisplayPatron();

        // Hide the order ticket until the patron is tapped.
        if (orderPanel != null)
            orderPanel.SetActive(levelManager.orderTaken);

        // Restore the ticket if the player returns from the kitchen.
        if (levelManager.orderTaken)
            DisplayOrder();
    }

    private void DisplayPatron()
    {
        if (levelManager.currentPatron == null)
            return;

        if (patronImage != null)
            patronImage.sprite = levelManager.currentPatron.patronSprite;

        if (patronNameText != null)
            patronNameText.text = levelManager.currentPatron.patronName;
    }

    public void OnPatronTapped()
    {
        levelManager.TakeOrder();

        if (!levelManager.orderTaken)
            return;

        DisplayOrder();

        if (orderPanel != null)
            orderPanel.SetActive(true);

        // Prevent taking the same order repeatedly.
        if (patronButton != null)
            patronButton.interactable = false;
    }

    private void DisplayOrder()
    {
        if (levelManager.currentDish == null)
            return;

        if (dishImage != null)
            dishImage.sprite = levelManager.currentDish.dishSprite;

        if (dishNameText != null)
            dishNameText.text = levelManager.currentDish.dishName;
    }

    public void GoToKitchen()
    {
        if (!levelManager.orderTaken)
        {
            Debug.Log("Tap the patron to take their order first!");
            return;
        }

        SceneManager.LoadScene("Kitchen");
    }
}
