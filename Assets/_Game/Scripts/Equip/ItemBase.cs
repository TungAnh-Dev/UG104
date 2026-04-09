using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    public int ID;
    public string ItemName;
    public ItemCategory Category;
    public Rarity Rarity;
    public Sprite Icon;
    public int maxStack = 99;
    public string Description;

    public List<StatEntry> Stats;

    public virtual void Use(StatCalculator calculator) { }
}
[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public int value;
}