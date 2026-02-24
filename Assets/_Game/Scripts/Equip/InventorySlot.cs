using NUnit.Framework.Interfaces;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemConfig item;
    public int amount;

    public bool IsEmpty => item == null;

    public void SetItem(ItemConfig newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
    }

    public void Clear()
    {
        item = null;
        amount = 0;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
        if (amount <= 0)
        {
            Clear();
        }
    }
}