using UnityEngine;
using UnityEngine.SceneManagement;

public class PatronSpriteChanger : MonoBehaviour
{
    public static PatronSpriteChanger Instance;

    public GameObject oldPatron;
    public GameObject newPatron;

    private void Awake()
    {
        // Prevent duplicate managers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (Instance == this)
            Instance = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the patrons in the newly loaded scene
        GameObject oldObject = GameObject.Find("OldPatron");
        GameObject newObject = GameObject.Find("NewPatron");

        if (oldObject != null)
            oldPatron = oldObject;

        if (newObject != null)
            newPatron = newObject;

        // Select the correct patron
        if (oldPatron != null && newPatron != null)
        {
            oldPatron.SetActive(!NewDay.level2);
            newPatron.SetActive(NewDay.level2);

            Debug.Log("Level 2: " + NewDay.level2);
            Debug.Log("Patron sprites updated!");
        }
    }
}