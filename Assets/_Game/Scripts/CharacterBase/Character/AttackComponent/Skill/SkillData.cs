using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/Skill")]
public class SkillData : ScriptableObject
{
    [Header("Info")]
    public string skillName;
    public string animationTrigger;
    public Sprite iconSkill;
    [Header("Prefab")]
    public GameObject skillPrefab;

    [Header("Stats")]
    public float damageMultiplier;
    public float speed;
    public float duration = 0.5f;

    [Header("Cooldown")]
    public float cooldown;
}
