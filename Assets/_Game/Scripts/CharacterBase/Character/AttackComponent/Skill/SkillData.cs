using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Game/Skill")]
public class SkillData : ScriptableObject
{
    [Header("Info")]
    public string skillName;
    public string animationTrigger;

    [Header("Prefab")]
    public GameObject skillPrefab;

    [Header("Stats")]
    public float damage;
    public float speed;
    public float duration = 0.5f;

    [Header("Cooldown")]
    public float cooldown;
}
