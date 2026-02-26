using System.Collections.Generic;

public class EquipmentManager
{
    public Dictionary<ItemType, EquipmentItem> Equipment { get; private set; }

    public EquipmentManager(Dictionary<ItemType, EquipmentItem> equipment)
    {
        Equipment = equipment;
    }

    public void Equip(EquipmentItem item)
    {
        Equipment[item.EquipType] = item;
    }

    public void Unequip(ItemType type)
    {
        Equipment[type] = null;
    }
}