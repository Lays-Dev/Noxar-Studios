
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GatheringMinigameManager : MonoBehaviour
{
    [Header("Available Ingredient GameObjects")]
    [Tooltip("Add every possible ingredient UI Image here.")]
    public List<TapImage> availableIngredients =
        new List<TapImage>();

    [Header("Gathering Completion")]
    public UnityEvent onGatheringCompleted;

    private void Start()
    {
        SetupGatheringMinigame();
    }

    private void SetupGatheringMinigame()
    {
        // Get the randomly selected dish.
        LevelManager levelManager = LevelManager.Instance;

        if (levelManager == null ||
            levelManager.currentDish == null)
        {
            Debug.LogError("Gathering: No current dish found!");
            return;
        }

        // Get the ingredients belonging to this dish.
        List<IngredientData> dishIngredients =
            levelManager.currentDish.ingredients;

        if (dishIngredients == null || dishIngredients.Count == 0)
        {
            Debug.LogError("This dish has no ingredients assigned!");
            return;
        }

        // Hide all available ingredient objects first.
        foreach (TapImage ingredient in availableIngredients)
        {
            if (ingredient != null)
            {
                ingredient.gameObject.SetActive(false);
            }
        }

        // Store the ingredient objects needed for this dish.
        List<TapImage> selectedIngredients = new List<TapImage>();

        // Find a matching GameObject for every dish ingredient.
        foreach (IngredientData dishIngredient in dishIngredients)
        {
            TapImage matchingIngredient = availableIngredients.Find(
                item => item != null &&
                string.Equals(
                    item.ingredientName,
                    dishIngredient.ingredientName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (matchingIngredient == null)
            {
                Debug.LogError(
                    "Could not find a Gathering GameObject for: " +
                    dishIngredient.ingredientName
                );

                continue;
            }

            selectedIngredients.Add(matchingIngredient);
        }

        if (selectedIngredients.Count == 0)
        {
            Debug.LogError("No matching Gathering ingredients were found!");
            return;
        }

        // Set up the ingredient sequence.
        for (int i = 0; i < selectedIngredients.Count; i++)
        {
            TapImage currentIngredient = selectedIngredients[i];

            GameObject next = null;

            // Find the next ingredient in the sequence.
            if (i < selectedIngredients.Count - 1)
            {
                next = selectedIngredients[i + 1].gameObject;
            }

            // Get the matching ingredient data.
            IngredientData data = dishIngredients.Find(
                ingredient => string.Equals(
                    ingredient.ingredientName,
                    currentIngredient.ingredientName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            currentIngredient.SetupIngredient(
                data,
                next,
                i == selectedIngredients.Count - 1,
                this
            );
        }

        // Activate the first ingredient.
        selectedIngredients[0].gameObject.SetActive(true);

        // Make sure its parent is active too.
        selectedIngredients[0].transform.parent.gameObject.SetActive(true);

        Debug.Log("First ingredient activated: " +
            selectedIngredients[0].ingredientName);

        Debug.Log("Ingredient active in hierarchy: " +
            selectedIngredients[0].gameObject.activeInHierarchy);

        Debug.Log(
            "Gathering started with " +
            selectedIngredients.Count +
            " ingredients for " +
            levelManager.currentDish.dishName
        );
    }

    // Called when the final ingredient has been gathered.
    public void FinishGathering()
    {
        Debug.Log("GATHERING MINIGAME COMPLETED!");

        onGatheringCompleted.Invoke();
    }
}
