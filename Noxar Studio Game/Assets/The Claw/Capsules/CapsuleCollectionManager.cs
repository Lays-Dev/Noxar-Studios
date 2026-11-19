using UnityEngine;
using System;
using System.Collections.Generic;

public class CapsuleCollectionManager : MonoBehaviour
{
    public static CapsuleCollectionManager Instance;

    [Header("All Available Capsules")]
    public List<CapsuleData> allCapsules =
        new List<CapsuleData>();

    // Stores each capsule and its collected amount.
    private Dictionary<CapsuleData, int> capsuleCounts =
        new Dictionary<CapsuleData, int>();

    // Notifies the collection screen when amounts change.
    public event Action OnCollectionChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddCapsule(CapsuleData capsuleData)
    {
        if (capsuleData == null)
        {
            Debug.LogWarning("Capsule data is missing!");
            return;
        }

        if (capsuleCounts.ContainsKey(capsuleData))
        {
            capsuleCounts[capsuleData]++;
        }
        else
        {
            capsuleCounts.Add(capsuleData, 1);
        }

        Debug.Log(
            capsuleData.capsuleName + " collected! Total: " +
            capsuleCounts[capsuleData]
        );

        // Tell the collection screen to update.
        OnCollectionChanged?.Invoke();
    }

    public int GetCapsuleCount(CapsuleData capsuleData)
    {
        if (capsuleData == null)
            return 0;

        if (capsuleCounts.ContainsKey(capsuleData))
            return capsuleCounts[capsuleData];

        return 0;
    }
}
