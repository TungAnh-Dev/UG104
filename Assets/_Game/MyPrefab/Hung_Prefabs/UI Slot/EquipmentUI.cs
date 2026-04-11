using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI Instance;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform equipmentArea;
    private Dictionary<ItemType, SlotUI> slotDictionary =
        new Dictionary<ItemType, SlotUI>();
    private void Awake()
    {
        Instance = this;
        //Khởi tạo các slot cho từng loại trang bị
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            CreateEquipmentSlot(type);
        }
    }
    private void CreateEquipmentSlot(ItemType type)
    {
        GameObject slot = Instantiate(slotPrefab, equipmentArea);

        slot.name = $"{type} Slot";

        SlotUI slotUI = slot.GetComponent<SlotUI>();
        slotUI.SetSlotEquipment(type);

        slotDictionary.Add(type, slotUI);
    }
    public void UpdateSlot(ItemType type, ItemBase item)
    {
        if (!slotDictionary.ContainsKey(type))
            return;

        SlotUI slotUI = slotDictionary[type];

        if (item == null)
        {
            slotUI.SetEquipmentItem(null);
        }
        else
        {
            slotUI.SetEquipmentItem(item);
        }
    }
}
