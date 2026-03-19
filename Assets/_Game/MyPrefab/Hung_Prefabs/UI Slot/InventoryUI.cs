using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;

    private void Start()
    {
        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (var slot in inventory.Slots)
        {
            InventorySlotUI newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.SetSlot(slot);
        }
    }
}
