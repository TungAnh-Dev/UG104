
using UnityEngine;

[System.Serializable]
public class Slot
{
    public string itemID;
    public int amount;

    public bool IsEmpty => string.IsNullOrEmpty(itemID);

    public void SetItem(string newItemID, int newAmount)
    {
        itemID = newItemID;
        amount = newAmount;
    }

    public void Clear()
    {
        itemID = null;
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