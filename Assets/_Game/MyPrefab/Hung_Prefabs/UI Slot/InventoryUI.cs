using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private Inventory_2 inventory;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform inventoryArea;
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
        for (int i = 0; i < inventory.GetSlotCount(); i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, inventoryArea);
            slotObj.name = $"Slot_{i + 1}";

            // Thiết lập slot là Inventory mode
            SlotUI slotUI = slotObj.GetComponent<SlotUI>();
            slotUI.SetSlotInventory();
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < inventory.GetSlotCount(); i++)
        {
            Slot dataSlot = inventory.GetSlot(i);
            SlotUI slotUI = inventoryArea.GetChild(i).GetComponent<SlotUI>();

            slotUI.UpdateInventorySlot(dataSlot);
        }
    }
}