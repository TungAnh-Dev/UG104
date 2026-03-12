using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class BaseSkill : MonoBehaviour
{
    protected SkillData data;
    protected Transform target;
    public virtual void Init(SkillData skillData)
    {
        data = skillData;
    }

    public virtual void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
