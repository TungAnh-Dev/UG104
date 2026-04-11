using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    private Dictionary<string, ItemBase> itemDictionary 
        = new Dictionary<string, ItemBase>();

    private void Awake()
    {
        Instance = this;
        LoadItems();
    }
    private void LoadItems()
    {
        ItemBase[] items = Resources.LoadAll<ItemBase>("Item");
        foreach (var item in items)
        {
            if (!itemDictionary.ContainsKey(item.ID))
            {
                itemDictionary.Add(item.ID, item);
            }
            else
            {
                Debug.LogWarning($"Duplicate item ID found: {item.ID}");
            }
        }
    }
    public ItemBase GetItemByID(string id)
    {
        itemDictionary.TryGetValue(id, out var item);
        return item;
    }
}
