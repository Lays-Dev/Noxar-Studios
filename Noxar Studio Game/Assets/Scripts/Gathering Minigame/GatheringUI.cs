using UnityEngine;
using System.Collections;
using TMPro;

public class GatheringUI : MonoBehaviour
{
    // This panel will close later when a button is pressed.
    [SerializeField] private GameObject instructionsPanel;

    [SerializeField] private GameObject countdownText;

    // Function that closes the panel - used for a button.
    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    // IEnumerator because this method will run for multiple frames
    private IEnumerator Countdown()
    {
        countdownText.SetActive(true);

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "2";

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "1";

        yield return new WaitForSeconds(1f);

        countdownText.GetComponent<TMP_Text>().text = "GO!";

        yield return new WaitForSeconds(1f);

        countdownText.SetActive(false);
    }
}
