/*
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2PhoneOrderButton : MonoBehaviour
{
    [Header("Button")]
    public GameObject buttonToActivate;

    [Header("Delivery Scene")]
    public string deliverySceneName = "Delivery";

    private void Start()
    {
        if (buttonToActivate != null)
        {
            buttonToActivate.SetActive(false);
        }
    }

    private void Update()
    {
        if (!LevelManager.level2Unlocked)
            return;

        bool phoneOrderReady =
            TapSprite.phoneOrderTaken &&
            TapSprite.phoneOrderGatherComplete &&
            TapSprite.phoneOrderGrillComplete;

        if (phoneOrderReady && buttonToActivate != null &&
            !buttonToActivate.activeSelf)
        {
            buttonToActivate.SetActive(true);

            Debug.Log("Deliver button unlocked!");
        }
    }

    // Connect this method to the Deliver button.
    public void DeliverOrder()
    {
        SceneManager.LoadScene(deliverySceneName);
    }
}
*/
