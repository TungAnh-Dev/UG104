using UnityEngine;
using System.Collections.Generic;

public class SkillSystem : MonoBehaviour 
{
    //Danh sách skill 
    [SerializeField]
    private List<SkillData> skillsList;

    private TargetSelector targetSelector;

    private AnimationComponent animationComponent;

    private void Awake()
    {
        animationComponent = GetComponent<AnimationComponent>();
        targetSelector = GetComponent<TargetSelector>();
    }

    public void UseSkill(int index)
    {
        if (skillsList == null || skillsList.Count == 0) return;
        if (index < 0 || index >= skillsList.Count) return;

        SkillData skill = skillsList[index];

        Debug.Log($"Sử dụng Skill {skill.name}");

        animationComponent.PlayTrigger(skill.animationTrigger);

        GameObject obj = Instantiate(
            skill.skillPrefab,
            transform.position + transform.forward,
            transform.rotation
        );

        // Gán hành vi cho đạn
        BaseSkill behaviour = obj.GetComponent<BaseSkill>();
        if( behaviour != null)
        {
            behaviour.Init(skill);
            Transform target = targetSelector.CurrentTarget;
            behaviour.SetTarget(target);
        }
    }

    
}
