using System.Collections.Generic;
using UnityEngine;

public class Inventory_2 : MonoBehaviour
{
    public static Inventory_2 Instance;

    [Header("Starting Items")]
    [SerializeField] private string[] startingItemIDs;

    [Header("Runtime Slots")]
    public List<Slot> slots = new List<Slot>();

    public int maxSlots = 30;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitInventory();
    }
    void InitInventory()
    {
        slots.Clear();

        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new Slot());
        }

        LoadStartingItems();

        RefreshUI();
    }

    void LoadStartingItems()
    {
        if (startingItemIDs == null) return;

        foreach (string id in startingItemIDs)
        {
            AddItem(id, 1);
        }
    }
    public Slot GetSlot(int index)
    {
        if (index < 0 || index >= slots.Count)
            return null;

        return slots[index];
    }

    public int GetSlotCount() 
    {
        return slots.Count;
    }

    public void AddItem(string itemID, int amount)
    {
        if (string.IsNullOrEmpty(itemID) || amount <= 0)
            return;

        ItemBase item = ItemDatabase.Instance.GetItemByID(itemID);

        if (item == null)
        {
            Debug.LogWarning($"Item not found: {itemID}");
            return;
        }
        // EQUIPMENT (NO STACK)
        if (item.Category == ItemCategory.Equipment)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].IsEmpty)
                {
                    slots[i].SetItem(itemID, 1);
                    RefreshUI();
                    return;
                }
            }

            Debug.Log("Inventory full (equipment)");
            return;
        }
        // CONSUMABLE (STACK FIRST)
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsEmpty && slots[i].itemID == itemID)
            {
                slots[i].AddAmount(amount);
                RefreshUI();
                return;
            }
        }
        // EMPTY SLOT
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].SetItem(itemID, amount);
                RefreshUI();
                return;
            }
        }

        Debug.Log("Inventory full!");
    }
    //Ham xoa item khoi inventory
    public void RemoveItem(string itemID, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemID == itemID)
            {
                slots[i].AddAmount(-amount);

                if (slots[i].amount <= 0)
                    slots[i].Clear();

                RefreshUI();
                return;
            }
        }
    }
    //Cap nhat lai giao dien sau khi thay doi inventory
    public void RefreshUI()
    {
        InventoryUI.Instance?.RefreshUI();
    }
}