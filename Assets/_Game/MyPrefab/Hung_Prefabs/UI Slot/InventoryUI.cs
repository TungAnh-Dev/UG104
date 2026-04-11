using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("References")]
    [SerializeField] private Inventory_2 inventory;
    [SerializeField] private Transform inventoryArea;
    [SerializeField] private GameObject slotPrefab;

    private List<SlotUI> slotUIs = new List<SlotUI>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateSlots();
        RefreshUI();
    }
    void CreateSlots()
    {
        slotUIs.Clear();
        //Lay so luong slot tu inventory va tao UI slot tuong ung
        int count = inventory.GetSlotCount();

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(slotPrefab, inventoryArea);

            obj.name = $"Slot_{i}";

            SlotUI ui = obj.GetComponent<SlotUI>();
            ui.SetSlotInventory();

            slotUIs.Add(ui);
        }
    }
    public void RefreshUI()
    {
        if (inventory == null)
        {
            Debug.LogError("Không tìm thấy inventory");
            return;
        }

        int count = Mathf.Min(inventory.GetSlotCount(), slotUIs.Count);

        for (int i = 0; i < count; i++)
        {
            Slot data = inventory.GetSlot(i);

            if (slotUIs[i] != null)
            {
                slotUIs[i].UpdateInventorySlot(data);
            }
        }
    }
}