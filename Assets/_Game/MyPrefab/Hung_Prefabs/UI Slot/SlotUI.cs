using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private GameObject highlight;

    private static SlotUI currentSelected;

    private Slot currentSlot;
    private ItemBase currentItem;
    private int amount;
    private SlotUIType mode;
    private ItemType equipmentType;

    // =========================
    // INVENTORY INIT
    // =========================
    public void SetSlotInventory()
    {
        mode = SlotUIType.Inventory;
        ClearSlot();
    }

    public void UpdateInventorySlot(Slot slot)
    {
        if (mode != SlotUIType.Inventory) return;

        currentSlot = slot;

        if (slot == null || slot.IsEmpty)
        {
            ClearSlot();
            return;
        }

        ItemBase item = ItemDatabase.Instance.GetItemByID(slot.itemID);
        if (item == null)
        {
            ClearSlot();
            return;
        }

        SetItem(item, slot.amount, SlotUIType.Inventory);
    }

    // =========================
    // EQUIPMENT INIT
    // =========================
    public void SetSlotEquipment(ItemType type)
    {
        mode = SlotUIType.Equipment;
        equipmentType = type;

        ClearSlot();

        contentText.gameObject.SetActive(true);
        contentText.text = type.ToString();
    }

    public void SetEquipmentItem(ItemBase item)
    {
        if (mode != SlotUIType.Equipment) return;

        if (item == null)
        {
            ClearSlot();
            SetSlotEquipment(equipmentType);
            return;
        }

        SetItem(item, 1, SlotUIType.Equipment);
    }

    // =========================
    // CORE SET ITEM (CHUNG)
    // =========================
    public void SetItem(ItemBase item, int amount, SlotUIType type)
    {
        currentItem = item;
        this.amount = amount;
        mode = type;

        if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.sprite = item.Icon;
        icon.enabled = true;

        amountText.gameObject.SetActive(amount > 1);
        amountText.text = amount.ToString();

        contentText.gameObject.SetActive(false);
    }

    // =========================
    // CLEAR
    // =========================
    public void ClearSlot()
    {
        currentItem = null;
        currentSlot = null;
        amount = 0;

        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }

        if (amountText != null)
            amountText.gameObject.SetActive(false);

        if (contentText != null && mode == SlotUIType.Inventory)
            contentText.gameObject.SetActive(false);
    }

    // =========================
    // CLICK
    // =========================
    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    void Select()
    {
        if (currentSelected != null)
            currentSelected.highlight.SetActive(false);

        currentSelected = this;

        if (highlight != null)
            highlight.SetActive(true);

        if (currentItem == null)
        {
            InfoItemUI.Instance.Clear();
            return;
        }

        InfoItemUI.Instance.ShowInfo(
            currentItem,
            amount,
            mode,
            currentSlot
        );
    }
}
public enum SlotUIType { Inventory, Equipment }