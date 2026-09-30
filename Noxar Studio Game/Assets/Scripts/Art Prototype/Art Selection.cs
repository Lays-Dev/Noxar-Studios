using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtSelection : MonoBehaviour
{
    [System.Serializable]
    public class ArtOption
    {
        public GameObject art;
        public Button button;
    }

    [SerializeField] private List<ArtOption> artOptions = new List<ArtOption>();
    [SerializeField] private float pageFlipDuration = 0.5f;

    void Start()
    {
        foreach (ArtOption option in artOptions)
        {
            option.art.SetActive(false);
            option.button.onClick.AddListener(() => ShowArt(option));
        }
    }

    async void ShowArt(ArtOption selected)
    {
        await Awaitable.WaitForSecondsAsync(pageFlipDuration);

        foreach (ArtOption option in artOptions)
        {
            option.art.SetActive(option == selected);
        }
    }
}