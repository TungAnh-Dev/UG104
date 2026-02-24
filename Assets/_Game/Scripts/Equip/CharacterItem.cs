using System.Collections.Generic;
using UnityEngine;

public class CharacterItem
{
    private Dictionary<StatType, int> BaseStats
        = new Dictionary<StatType, int>();

    public Dictionary<ItemType, ItemConfig> Equipment
        = new Dictionary<ItemType, ItemConfig>();

    public Dictionary<StatType, int> TotalStats { get; private set; }

    public void CalculateStats()
    {
        TotalStats = new Dictionary<StatType, int>();

        foreach (var stat in BaseStats)
        {
            TotalStats[stat.Key] = stat.Value;
        }
        foreach (var item in Equipment.Values)
        {
            foreach (var stat in item.Stats)
            {
                StatType statType = stat.statType;
                int rawValue = stat.value;
                float rarityBonus = BuffRarity.GetBonusPercent(item.Rarity);
                int finalValue = (int)(rawValue * (1 + rarityBonus));
                if (!TotalStats.ContainsKey(statType))
                {
                    TotalStats[statType] = 0;
                }

                TotalStats[statType] += finalValue;

            }
        }
    }
}
