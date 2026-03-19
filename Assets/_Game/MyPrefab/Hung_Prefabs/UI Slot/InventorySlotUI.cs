using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;

    public void SetSlot(InventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            icon.enabled = false;
            amountText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = slot.item.Icon;
            amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";
        }
    }
}
