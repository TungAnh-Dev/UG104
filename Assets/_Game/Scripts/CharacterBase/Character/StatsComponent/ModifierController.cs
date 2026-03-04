using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ModifierController : MonoBehaviour
{
    private StatsComponent stats;

    private List<StatsModifier> activeModifiers = new List<StatsModifier>();

    private void Awake()
    {
        stats = GetComponent<StatsComponent>();
    }

    public void AddModifier(StatsModifier modifier)
    {
        activeModifiers.Add(modifier);
        stats.AddModifier(modifier);
    }

    public void RemoveModifier(StatsModifier modifier)
    {
        if (activeModifiers.Contains(modifier))
        {
            activeModifiers.Remove(modifier);
            stats.RemoveModifier(modifier);
        }
    }

    public void ClearAll()
    {
        foreach (var modifier in activeModifiers)
        {
            stats.RemoveModifier(modifier);
        }

        activeModifiers.Clear();
    }
}
