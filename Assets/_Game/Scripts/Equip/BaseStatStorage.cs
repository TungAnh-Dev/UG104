using System.Collections.Generic;

public class BaseStatStorage
{
    public Dictionary<StatType, int> BaseStats { get; private set; }

    public BaseStatStorage(Dictionary<StatType, int> baseStats)
    {
        BaseStats = new Dictionary<StatType, int>(baseStats);
    }
}