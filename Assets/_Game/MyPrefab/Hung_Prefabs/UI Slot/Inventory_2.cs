using System.Collections.Generic;
using UnityEngine;

public class Inventory_2 : MonoBehaviour
{
    public static Inventory_2 Instance;
    [Header("Starting Items")]
    [SerializeField] private List<ItemBase> startingItems;

    [Header("Runtime Slots")]
    public List<Slot> slots = new List<Slot>();

    public int maxSlots = 30;

    private void Awake()
    {
        InitInventory();
    }

    void InitInventory()
    {
        slots.Clear();

        // Tạo slot rỗng trước
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new Slot());
        }

        //Thêm 5 item đầu tiên vào inventory
        foreach (var item in startingItems)
        {
            AddItem(item, 1); // mỗi item 1 cái
        }
    }

    public int GetSlotCount()
    {
        return slots.Count;
    }

    public Slot GetSlot(int index)
    {
        return slots[index];
    }

    // Thêm item (stack nếu có, nếu không thì tìm slot trống)
    public bool AddItem(ItemBase item, int amount)
    {
        // 🧪 Nếu là Consumable → được stack
        if (item.Category == ItemCategory.Consumable)
        {
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty && slot.item == item)
                {
                    int spaceLeft = item.maxStack - slot.amount;

                    if (spaceLeft > 0)
                    {
                        int addAmount = Mathf.Min(spaceLeft, amount);
                        slot.AddAmount(addAmount);

                        return true;
                    }
                }
            }
        }

        // 🪖 Equipment hoặc không stack được → mỗi slot 1 cái
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                int addAmount = item.Category == ItemCategory.Equipment ? 1 : amount;
                slot.SetItem(item, addAmount);

                return true;
            }
        }

        return false;
    }
    public void RemoveItem(ItemBase item, int amount)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.amount -= amount;

                if (slot.amount <= 0)
                    slot.Clear();

                break;
            }
        }

        InventoryUI.Instance.RefreshUI();
    }
}