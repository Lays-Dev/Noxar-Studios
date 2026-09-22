using UnityEngine;
using UnityEngine.UI;


public class ArtSelection : MonoBehaviour
{

    [SerializeField] private GameObject playerArt;
    [SerializeField] private GameObject art2;
    public Button myButton;
    public Button button2;

    void Start()
    {
        playerArt.SetActive(false);
        art2.SetActive(false);
        button2.onClick.AddListener(Button2);
         myButton.onClick.AddListener(ButtonClick);
    }

 
    async void ButtonClick()
    {
        await Awaitable.NextFrameAsync();
        await Awaitable.NextFrameAsync();
        playerArt.SetActive(true);
        art2.SetActive(false);
    }

    void Button2()
    {
        art2.SetActive(true);
        playerArt.SetActive(false);
    }
 
}
