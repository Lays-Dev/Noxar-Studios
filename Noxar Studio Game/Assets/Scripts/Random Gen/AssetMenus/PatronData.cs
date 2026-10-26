using UnityEngine;

// Creates a menu in the project window.
// Creates assets without writing more code.
// The menu option is called Cooking Game Patron
// To use it:
// 1. Go to the Assets/Art/Patrons folder.
// 2. Right-click in the Project window.
// 3. Select Create > Cooking Game > Patron.
// 4. A new Patron asset will be created.
[CreateAssetMenu(fileName = "New Patron", menuName = "Cooking Game/Patron")]
public class PatronData : ScriptableObject
{
    [Header("Patron Information")]
    public string patronName;
    public Sprite patronSprite;
}

