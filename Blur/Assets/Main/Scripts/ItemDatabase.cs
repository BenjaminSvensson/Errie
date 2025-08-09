using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemPrefabEntry
{
    public string itemID;
    public GameObject prefab;
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    [SerializeField] private List<ItemPrefabEntry> itemPrefabs;

    private Dictionary<string, GameObject> itemLookup;

    private void Awake()
    {
        Instance = this;
        itemLookup = new Dictionary<string, GameObject>();

        foreach (var entry in itemPrefabs)
        {
            if (!itemLookup.ContainsKey(entry.itemID))
                itemLookup.Add(entry.itemID, entry.prefab);
        }
    }

    public GameObject GetItemPrefab(string itemID)
    {
        itemLookup.TryGetValue(itemID, out GameObject prefab);
        return prefab;
    }
}
