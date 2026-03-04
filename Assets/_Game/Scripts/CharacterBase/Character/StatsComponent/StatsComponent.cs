using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    [Header("Stats Data")]
    [SerializeField]
    private CharacterStatsData data;

    //BaseStat lay tu SO
    private Dictionary<StatsType, float> baseStats = new Dictionary<StatsType, float>();
    // Modifier runtime
    private List<StatsModifier> modifiersList = new List<StatsModifier>();
    private void Awake()
    {
        InitBaseStats();
    }

    private void InitBaseStats()
    {
        if(data == null)
        {
            Debug.LogWarning($"{gameObject.name} chua gan Data");
        }
        baseStats = data.GetBaseStats();
    }
    //Modifier
    public void AddModifier(StatsModifier modifier)
    {
        modifiersList.Add(modifier);
    }
    public void RemoveModifier(StatsModifier modifier)
    {
        modifiersList.Remove(modifier);
    }
    public float CalculateFinalStat(StatsType statType, float baseValue)
    {
        float flatBonus = 0f;
        float percentBonus = 0f;

        foreach (var modifier in modifiersList)
        {
            if (modifier.StatType != statType)
                continue;

            if (modifier.Type == ModifierType.Flat)
                flatBonus += modifier.Value;

            if (modifier.Type == ModifierType.Percent)
                percentBonus += modifier.Value;
        }

        float finalValue = (baseValue + flatBonus) * (1 + percentBonus);
        return Mathf.Max(0,finalValue);
    }
    //GetStat
    public float GetStat(StatsType statType)
    {
        float baseValue = baseStats.TryGetValue(statType, out float value) ? value : 0f;
        return CalculateFinalStat(statType, baseValue);
    }
}
