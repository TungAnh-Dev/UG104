using System.Collections.Generic;

public class ConsumableHandler
{
    public void Apply(
        ConsumableItem item,
        Dictionary<StatType, int> totalStats,
        HPHandler hpHandler)
    {
        if (item == null || item.Stats == null)
            return;

        foreach (var effect in item.Stats)
        {
            if (effect.statType == StatType.HP)
            {
                int maxHP = totalStats.ContainsKey(StatType.HP)
                    ? totalStats[StatType.HP]
                    : 0;

                hpHandler.Heal(effect.value, maxHP);
            }
            else
            {
                if (!totalStats.ContainsKey(effect.statType))
                    totalStats[effect.statType] = 0;

                totalStats[effect.statType] += effect.value;
            }
        }
    }
}