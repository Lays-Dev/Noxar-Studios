using UnityEngine;
using System.Collections;
using TMPro;

public class GatheringUI : MonoBehaviour
{
    // This panel will close later when a button is pressed.
[SerializeField] private GameObject instructionsPanel;
[SerializeField] private GameObject countdownText;
[SerializeField] private GameObject tapBox;
[SerializeField] private RectTransform minigameArea;
[SerializeField] private GameObject minigamePanel;

[SerializeField] private int boxesRequired = 3;

private int boxesCompleted = 0;

    // Function that closes the panel - used for a button.
    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
        StartCoroutine(Countdown());
        //Countdown();
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


        countdownText.SetActive(false);
        yield return new WaitForSeconds(1f);

countdownText.SetActive(false);

tapBox.SetActive(true);
    }

public void SpawnNewBox()
{
    boxesCompleted++;

    if (boxesCompleted >= boxesRequired)
    {
        EndMinigame();
        return;
    }

    tapBox.SetActive(true);
    Debug.Log("TapBox Button Appered!");

    RectTransform box = tapBox.GetComponent<RectTransform>();

    float halfWidth = box.rect.width / 2;
    float halfHeight = box.rect.height / 2;

    float x = Random.Range(
        -minigameArea.rect.width / 2 + halfWidth,
        minigameArea.rect.width / 2 - halfWidth
    );

    float y = Random.Range(
        -minigameArea.rect.height / 2 + halfHeight,
        minigameArea.rect.height / 2 - halfHeight
    );

    box.anchoredPosition = new Vector2(x, y);
}

private void EndMinigame()
{
    Debug.Log("Minigame Complete!");
    tapBox.SetActive(false);
    minigamePanel.SetActive(false);
}
}
