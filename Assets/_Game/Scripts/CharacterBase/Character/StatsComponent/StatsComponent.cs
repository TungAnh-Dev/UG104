using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    //Lấy các thuộc tính đưa vào Dictionary
    private Dictionary<StatsType, float> BaseStats = new Dictionary<StatsType, float>();
    //Thêm các modifier vào List
    private List<StatsModifier> Modifiers = new List<StatsModifier>();

    //Hàm setup giá trị cho các thuộc tính cơ bản
    public void SetBaseStats(StatsType statType, float value)
    {
        BaseStats[statType] = value;
    }
    // Hàm thêm modifier vào List
    public void AddModifier(StatsModifier modifier)
    {
        Modifiers.Add(modifier);
    }
    //Hàm remove modifier khỏi List
    public void RemoveModifier(StatsModifier modifier)
    {
        Modifiers.Remove(modifier);
    }
    //Hàm trả về giá trị cuối cùng của thuộc tính
    public float GetStat(StatsType statType)
    {
        //Lấy giá  trị cơ bản của thuộc tính
        //Nếu thuộc tính không có trong Dict thì trả về 0
        float baseValue = BaseStats.ContainsKey(statType) ? BaseStats[statType] : 0f;
        float flat = 0f;
        float percent = 0f;
        foreach (var modifier in Modifiers)
        {
            if (modifier.StatType != statType) continue;

            if (modifier.Type == ModifierType.Flat)
            {
                flat = flat + modifier.Value;
            }
            if (modifier.Type == ModifierType.Percent)
            {
                percent = percent + modifier.Value;
            }
        }
        return (baseValue + flat) * (1 + percent);
    }
}
