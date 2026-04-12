using UnityEngine;



[CreateAssetMenu(menuName = "Item/Equipment")]
public class EquipmentItem : ItemBase
{
    public ItemType EquipType;
    public WeaponType WeaponType;

    [Header("Skill")]
    public SkillData skillData;
}
