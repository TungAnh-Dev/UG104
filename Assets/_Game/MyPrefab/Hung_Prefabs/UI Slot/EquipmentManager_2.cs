using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager_2 : MonoBehaviour
{
    public static EquipmentManager_2 Instance;
    [Header("Equipped Items")]
    private Dictionary<ItemType, ItemBase> equippedItems = new Dictionary<ItemType, ItemBase>();

    private void Awake()
    {
        Instance = this;
    }
    //Hàm equip trang bị
    public void Equip(Slot currentSlot)
    {
        //
        if (currentSlot == null || currentSlot.IsEmpty) return;
        Debug.Log("Trying to equip item from slot...");

        //Get item from database
        ItemBase item = ItemDatabase.Instance.GetItemByID(currentSlot.itemID);

        if (item == null) return;

        if (item is not EquipmentItem equipItem)
        {
            Debug.LogWarning("Item is not Equipment!");
            return;
        }

        ItemType type = equipItem.EquipType;

        //Nếu đã có item cùng loại được trang bị sẽ trả về inventory
        if (equippedItems.ContainsKey(type))
        {
            ItemBase oldItem = equippedItems[type];

            Inventory_2.Instance.AddItem(oldItem.ID, 1);
        }

        //Trang bị item mới
        equippedItems[type] = item;


        // update UI
        EquipmentUI.Instance.UpdateSlot(type, item);
        Inventory_2.Instance.RemoveItem(item.ID, 1);
        InventoryUI.Instance.RefreshUI();
        FemaleKnight.Instance.HandleEquipment(item.itemPrefab);
        Debug.Log($"Equipped: {item.name}");
    }

    // Hàm unequip trang bị
    public void Unequip(ItemType type)
    {
        if (!equippedItems.ContainsKey(type))
            return;

        ItemBase item = equippedItems[type];

        equippedItems.Remove(type);

        // trả về inventory
        Inventory_2.Instance.AddItem(item.ID, 1);

        // update UI
        EquipmentUI.Instance.UpdateSlot(type, null);
        FemaleKnight.Instance.HandleUnequipment();
        Debug.Log($"Unequipped: {item.name}");
    }

    // Kiểm tra item đã được trang bị chưa
    public ItemBase GetEquippedItem(ItemType type)
    {
        equippedItems.TryGetValue(type, out ItemBase item);
        return item;
    }

    // Kiểm tra nếu đã trang bị item cùng loại
    public bool IsEquipped(ItemType type)
    {
        return equippedItems.ContainsKey(type);
    }
    
}
