using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    protected SkillData data;
    protected Transform target;
    protected StatsComponent ownerStats;
    public virtual void Init(SkillData skillData, StatsComponent ownerStats)
    {
        data = skillData;
        this.ownerStats = ownerStats;
    }

    public virtual void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public abstract void Use();
}
