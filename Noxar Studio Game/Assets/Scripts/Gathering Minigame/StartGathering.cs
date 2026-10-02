using UnityEngine;
using UnityEngine.InputSystem;

public class StartGathering : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");

        canvas.SetActive(true);
        player.SetActive(false);
    }
}
