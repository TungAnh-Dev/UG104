using System.Collections.Generic;

public class EquipmentStatCalculator
{
    public Dictionary<StatType, int> Calculate(
        Dictionary<StatType, int> baseStats,
        Dictionary<ItemType, EquipmentItem> equipment)
    {
        Dictionary<StatType, int> total =
            new Dictionary<StatType, int>(baseStats);

        foreach (var item in equipment.Values)
        {
            if (item == null || item.Stats == null)
                continue;

            foreach (var stat in item.Stats)
            {
                if (!total.ContainsKey(stat.statType))
                    total[stat.statType] = 0;

                float rarityBonus =
                    BuffRarity.GetBonusPercent(item.Rarity);

                int finalValue =
                    (int)(stat.value * (1 + rarityBonus));

                total[stat.statType] += finalValue;
            }
        }

        return total;
    }
}