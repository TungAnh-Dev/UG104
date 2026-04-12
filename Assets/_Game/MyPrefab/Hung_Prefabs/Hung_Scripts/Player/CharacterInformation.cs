using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    public static CharacterInformation Instance;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] CharacterStatsData characterStatsData;
    [SerializeField] private StatsComponent statsComponent;
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUI()
    {
        if (infoText == null) return;

        string info = "";

        if(statsComponent != null)
        {
            info += $"Health: {statsComponent.GetStat(StatsType.MaxHP)}\n";
            info += $"Attack: {statsComponent.GetStat(StatsType.Attack)}\n";
            info += $"Defense: {statsComponent.GetStat(StatsType.Defense)}\n";
            info += $"Movespeed: {statsComponent.GetStat(StatsType.MoveSpeed)}\n";
        }
        else
        {
            info = "StatsComponent chua gan!";
        }

        infoText.text = info;
    }

    private void Start()
    {
            UpdateUI();
    }
}
