using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager_2 : MonoBehaviour
{
    public static EquipmentManager_2 Instance;
    [Header("Equipped Items")]
    private Dictionary<ItemType, ItemBase> equippedItems = new Dictionary<ItemType, ItemBase>();
    // Thêm biến lấy StatsComponent từ nhân vật
    [SerializeField] private StatsComponent playerStats;
    [SerializeField] private SkillSystem skillSystem;
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
            //Xóa Stats của item cũ
            playerStats.RemoveAllModifiersFromSource(oldItem);
            Inventory_2.Instance.AddItem(oldItem.ID, 1);
        }

        //Trang bị item mới
        equippedItems[type] = item;
        //Thêm Stats của item mới
        foreach (var stat in item.Stats)
        {
            StatsModifier modifier = new StatsModifier(
                stat.statType,
                stat.value,
                ModifierType.Flat,
                item
            );
            playerStats.AddModifier(modifier);
        }
        
        // update UI
        EquipmentUI.Instance.UpdateSlot(type, item);
        Inventory_2.Instance.RemoveItem(item.ID, 1);
        InventoryUI.Instance.RefreshUI();
        CharacterInformation.Instance.UpdateUI();
        FemaleKnight.Instance.HandleEquipment(item.itemPrefab);
        Debug.Log($"Equipped: {item.name}");
        if (equipItem.EquipType == ItemType.Weapon)
        {
            Debug.Log("Đang gán skill: " + equipItem.skillData);
            skillSystem.SetCurrentSkill(equipItem.skillData);
        }
        Debug.Log("Current ATK: " + playerStats.GetStat(StatsType.Attack));
    }

    // Hàm unequip trang bị
    public void Unequip(ItemType type)
    {
        if (!equippedItems.ContainsKey(type))
            return;

        ItemBase item = equippedItems[type];
        playerStats.RemoveAllModifiersFromSource(item);

        equippedItems.Remove(type);

        // trả về inventory
        Inventory_2.Instance.AddItem(item.ID, 1);

        // update UI
        EquipmentUI.Instance.UpdateSlot(type, null);
        FemaleKnight.Instance.HandleUnequipment();
        if (type == ItemType.Weapon)
        {
            skillSystem.SetCurrentSkill(null);
        }
        Debug.Log($"Unequipped: {item.name}");
        CharacterInformation.Instance.UpdateUI();
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
