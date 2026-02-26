using System.Collections.Generic;

public class StatCalculator
{
    private BaseStatStorage baseStorage;
    private EquipmentManager equipmentManager;
    private EquipmentStatCalculator statCalculator;
    private ConsumableHandler consumableHandler;
    private HPHandler hpHandler;

    public Dictionary<StatType, int> TotalStats { get; private set; }

    public int MaxHP =>
        TotalStats.ContainsKey(StatType.HP)
        ? TotalStats[StatType.HP]
        : 0;

    public int CurrentHP => hpHandler.CurrentHP;

    public StatCalculator(
        Dictionary<StatType, int> baseStats,
        Dictionary<ItemType, EquipmentItem> equipment)
    {
        baseStorage = new BaseStatStorage(baseStats);
        equipmentManager = new EquipmentManager(equipment);
        statCalculator = new EquipmentStatCalculator();
        consumableHandler = new ConsumableHandler();
        hpHandler = new HPHandler();

        Recalculate();
        hpHandler.Initialize(MaxHP);
    }

    public void Recalculate()
    {
        TotalStats = statCalculator.Calculate(
            baseStorage.BaseStats,
            equipmentManager.Equipment);

        hpHandler.ClampToMax(MaxHP);
    }

    public void Equip(EquipmentItem item)
    {
        equipmentManager.Equip(item);
        Recalculate();
    }

    public void Unequip(ItemType type)
    {
        equipmentManager.Unequip(type);
        Recalculate();
    }

    public void UseConsumable(ConsumableItem item)
    {
        consumableHandler.Apply(item, TotalStats, hpHandler);
    }
}