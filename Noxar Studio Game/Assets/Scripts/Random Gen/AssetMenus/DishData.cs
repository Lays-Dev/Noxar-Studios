// Creates a menu in the project window.
// Creates assets without writing more code.
// The menu option is called Cooking Game Dish
// To use it:
// 1. Go to the Assets/Art/Dishes folder.
// 2. Right-click in the Project window.
// 3. Select Create > Cooking Game > Dish.
// 4. A new Dish asset will be created.
using System.Collections.Generic;
using UnityEngine;

// Stores the three visual stages of an ingredient.
[System.Serializable]
public class IngredientData
{
    public string ingredientName;

    [Header("Ingredient Sprites")]
    public Sprite uncutSprite;
    public Sprite cutSprite;
    public Sprite badlyCutSprite;
}

[CreateAssetMenu(fileName = "New Dish", menuName = "Cooking Game/Dish")]
public class DishData : ScriptableObject
{
    [Header("Dish Information")]
    public string dishName;
    public Sprite dishSprite;

    [Header("Animated Dish VFX")]
    [Tooltip("Add the animation frames in playback order.")]
    public Sprite[] dishVFXFrames;

    [Tooltip("How many animation frames play per second.")]
    public float vfxFramesPerSecond = 12f;

    [Tooltip("Should the VFX animation repeat?")]
    public bool loopVFX = true;

    [Header("Dish Ingredients")]
    public List<IngredientData> ingredients = new List<IngredientData>(3);
}
