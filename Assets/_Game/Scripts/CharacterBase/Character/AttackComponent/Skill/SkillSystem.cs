using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private SkillButtonUI skillButtonUI;
    [SerializeField] private Transform skillSpawnPoint;
    private TargetSelector targetSelector;
    private AnimationComponent animationComponent;
    private StatsComponent statsComponent;

    private SkillData currentSkill;

    private void Awake()
    {
        animationComponent = GetComponent<AnimationComponent>();
        targetSelector = GetComponent<TargetSelector>();
        statsComponent = GetComponent<StatsComponent>();
    }

    private void Start()
    {
        if (skillButtonUI != null)
            skillButtonUI.Init(this);
    }

    // Khi equip
    public void SetCurrentSkill(SkillData skillData)
    {
        Debug.Log("SetCurrentSkill trên object: " + gameObject.name);

        currentSkill = skillData;

        if (skillButtonUI != null)
        {
            if (currentSkill != null)
                skillButtonUI.SetSkill(currentSkill);
            else
                skillButtonUI.SetSkill(null);
        }
    }

    public void UseCurrentSkill()
    {
        Debug.Log("UseCurrentSkill trên object: " + gameObject.name);
        if (currentSkill == null)
        {
            Debug.Log("Chưa có skill!");
            return;
        }

        GameObject obj = Instantiate(
            currentSkill.skillPrefab,
            skillSpawnPoint.position,
            skillSpawnPoint.rotation
        );

        BaseSkill behaviour = obj.GetComponent<BaseSkill>();

        if (behaviour != null)
        {
            behaviour.Init(currentSkill, statsComponent);
            behaviour.SetTarget(targetSelector?.CurrentTarget);
            behaviour.Use();
        }
    }
}