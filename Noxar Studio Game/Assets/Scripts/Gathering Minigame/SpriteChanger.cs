using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{

    public Image ingredientImage;

    public Sprite glorboGoo;
    public Sprite stardustShaker;
    public Sprite powerCell;

    public int currentSprite = 0;

    public void ChangeSprite()
    {
        currentSprite++;

        if (currentSprite > 2)
        {
            currentSprite = 0;
        }

        if (currentSprite == 0)
        {
            ingredientImage.sprite = glorboGoo;
        }
        else if (currentSprite == 1)
        {
            ingredientImage.sprite = stardustShaker;
        }
        else if (currentSprite == 2)
        {
            ingredientImage.sprite = powerCell;
        }
    }

}
