// Creates a menu in the project window.
// Creates assets without writing more code.
// The menu option is called Cooking Game Minigame
// To use it:
// 1. Go to the Assets/Scenes/Minigames folder.
// 2. Right-click in the Project window.
// 3. Select Create > Cooking Game > Minigame.
// 4. A new Minigame asset will be created.
using UnityEngine;

[CreateAssetMenu(fileName = "New Minigame", menuName = "Cooking Game/Minigame")]
public class MinigameData : ScriptableObject
{
    [Header("Minigame Information")]
    public string minigameName;
    public string minigameID;

    [Header("Minigame UI")]
    [Tooltip("Prefab containing this minigame's UI.")]
    public GameObject minigamePrefab;

    [Header("Random Generation")]
    public bool availableForRandomSelection = true;
}
