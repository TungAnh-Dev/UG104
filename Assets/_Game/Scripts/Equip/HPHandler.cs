using UnityEngine;

public class HPHandler
{
    public int CurrentHP { get; private set; }

    public void Initialize(int maxHP)
    {
        CurrentHP = maxHP;
    }

    public void Heal(int amount, int maxHP)
    {
        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHP);
    }

    public void ClampToMax(int maxHP)
    {
        CurrentHP = Mathf.Clamp(CurrentHP, 0, maxHP);
    }
}