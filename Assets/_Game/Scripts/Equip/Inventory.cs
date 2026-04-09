using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotCount = 50;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    public IReadOnlyList<InventorySlot> Slots => slots;

    void Awake()
    {
        InitializeSlots();
    }

    void InitializeSlots()
    {
        slots.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public bool AddItem(ItemBase item, int amount = 1)
    {
        
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                slot.AddAmount(amount);
                return true;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetItem(item, amount);
                return true;
            }
        }

        return false;
    }

    public void RemoveItem(ItemBase item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty && slot.item == item)
            {
                slot.RemoveAmount(amount);
                return;
            }
        }
    }

    
}
public enum SlotType
{
    Equipment,
    Inventory
}