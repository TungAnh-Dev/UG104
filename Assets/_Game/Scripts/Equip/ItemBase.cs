using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    public string ID;
    public string ItemName;
    public ItemCategory Category;
    public Rarity Rarity;
    public Sprite Icon;
    public int maxStack = 99;
    public string Description;

    public GameObject itemPrefab;

    public List<StatEntry> Stats;

}
[System.Serializable]
public class StatEntry
{
    public StatsType statType;
    public int value;
}