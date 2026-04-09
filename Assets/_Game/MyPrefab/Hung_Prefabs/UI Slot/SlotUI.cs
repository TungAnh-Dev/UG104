using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private GameObject highlight;

    private static SlotUI currentSelected;
    private Slot currentData; // 
    private ItemBase equipmentItem;

    private SlotUIType mode;
    private ItemType equipmentType;

    //Thiết lập slot cho Inventory
    public void SetSlotInventory()
    {
        mode = SlotUIType.Inventory;
        ClearSlot();
        
    }

    //Thiết lập slot cho Equipment
    public void SetSlotEquipment(ItemType type)
    {
        mode = SlotUIType.Equipment;
        equipmentType = type;

        ClearSlot();

        contentText.gameObject.SetActive(true);
        contentText.text = type.ToString();
        contentText.color = Color.black;
    }
    
    public void UpdateInventorySlot(Slot slot)
    {
        if(mode != SlotUIType.Inventory) return;

        currentData = slot;

        if (slot.IsEmpty)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        icon.sprite = slot.item.Icon;

        if(slot.item.Category == ItemCategory.Consumable && slot.amount > 1)
        {
            amountText.gameObject.SetActive(true);
            amountText.text = slot.amount.ToString();
        }
        else
        {
            amountText.gameObject.SetActive(false);
        }

       
    }

    public void SetEquipmentItem(ItemBase item)
    {
        if(mode != SlotUIType.Equipment) return;
        if (item == null)
        {
            SetSlotEquipment(equipmentType);
            return;
        }
        icon.enabled = true;
        icon.sprite = item.Icon;

        contentText.gameObject.SetActive(true);
        amountText.gameObject.SetActive(false);
    }

    //Clear slot (dùng cho cả Inventory và Equipment)
    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
        amountText.gameObject.SetActive(false);
        contentText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    void Select()
    {
        // Tắt highlight slot cũ
        if (currentSelected != null)
            currentSelected.highlight.SetActive(false);

        currentSelected = this;
        highlight.SetActive(true);
        if (mode == SlotUIType.Inventory)
        {
            if (currentData != null && !currentData.IsEmpty)
                InfoItemUI.Instance.ShowInfo(currentData.item, currentData.amount, mode);
            else
                InfoItemUI.Instance.Clear();
        }
        else if (mode == SlotUIType.Equipment)
        {
            if (equipmentItem != null)
                InfoItemUI.Instance.ShowInfo(equipmentItem, 1, mode);
            else
                InfoItemUI.Instance.Clear();
        }
    }
}

public enum SlotUIType
{
    Inventory,
    Equipment
}