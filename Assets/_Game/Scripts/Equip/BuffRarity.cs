using UnityEngine;

public static class BuffRarity
{

    public static float GetBonusPercent(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return 0f;
            case Rarity.Uncommon:
                return 0.05f;
            case Rarity.Rare:
                return 0.1f;
            case Rarity.Epic:
                return 0.20f;
            case Rarity.Legendary:
                return 0.40f;
            default:
                return 0f;

        }
    }
}
