using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsData", menuName = "GameRPG-Data/CharacterStatsData")]
public class CharacterStatsData : ScriptableObject
{
    [System.Serializable]
    public class StatsData
    {
        public StatsType statType;
        public float value;
    }

    [Header("Base Stats")]
    [SerializeField] private List<StatsData> baseStats = new List<StatsData>();

    // Trả về dictionary để StatsComponent dùng
    public Dictionary<StatsType, float> GetBaseStats()
    {
        Dictionary<StatsType, float> statsDictionary = new Dictionary<StatsType, float>();

        foreach (var stat in baseStats)
        {
            if (!statsDictionary.ContainsKey(stat.statType))
            {
                statsDictionary.Add(stat.statType, stat.value);
            }
        }

        return statsDictionary;
    }
}
