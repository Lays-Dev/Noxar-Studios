using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButton : MonoBehaviour
{

    public GameObject deliveryPanel;
   public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DeliveryButtonClicked()
    {
        deliveryPanel.SetActive(true);
    }
}
