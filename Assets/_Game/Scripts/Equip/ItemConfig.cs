using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig")]
public class ItemConfig : ScriptableObject
{
    public int id;
    public string Name;
    public ItemType Type;
    public Rarity Rarity;
    public List<StatEntry> Stats;


}
[System.Serializable]
public class StatEntry
{
    public StatType statType;
    public int value;
}



