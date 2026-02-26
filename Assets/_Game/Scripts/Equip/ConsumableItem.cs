using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class ConsumableItem : ItemBase
{
    public override void Use(StatCalculator calculator)
    {
        calculator.UseConsumable(this);
    }
}
