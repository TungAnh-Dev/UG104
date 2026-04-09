using UnityEngine;

[System.Serializable]
public class Slot
{
    public ItemBase item;
    public int amount;

    public bool IsEmpty => item == null;

    public void SetItem(ItemBase newItem, int newAmount)
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