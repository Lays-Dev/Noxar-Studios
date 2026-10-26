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

[CreateAssetMenu(fileName = "New Dish", menuName = "Cooking Game/Dish")]
public class DishData : ScriptableObject
{
    [Header("Dish Information")]
    public string dishName;
    public Sprite dishSprite;

    //[Header("Future Customization")]
    //[Tooltip("Optional minigames to use as preferences in a future version.")]
    //public List<MinigameData> preferredMinigames =
        //new List<MinigameData>();
}
