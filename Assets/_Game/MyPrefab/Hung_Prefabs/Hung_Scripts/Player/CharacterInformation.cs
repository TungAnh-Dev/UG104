using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterInformation : MonoBehaviour
{
    public static CharacterInformation Instance;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] CharacterStatsData characterStatsData;
    [SerializeField] private StatsComponent statsComponent;

    private HealthComponent healthComponent;
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
            float currentHP = healthComponent.GetCurrentHealth();
            
            info += $"Health: {currentHP}\n";
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

    private void Update()
    {
        if (FemaleKnight.Instance != null)
        {
            statsComponent = FemaleKnight.Instance.GetComponent<StatsComponent>();
            healthComponent = FemaleKnight.Instance.GetComponent<HealthComponent>();

            if (statsComponent != null)
                statsComponent.OnStatsChanged += UpdateUI;

            if (healthComponent != null)
                healthComponent.OnHealthChanged += UpdateUI;
        }

        UpdateUI();
    }
    private void OnDisable()
    {
        if (statsComponent != null)
            statsComponent.OnStatsChanged -= UpdateUI;
        if (healthComponent != null)
            healthComponent.OnHealthChanged -= UpdateUI;
    }
}
