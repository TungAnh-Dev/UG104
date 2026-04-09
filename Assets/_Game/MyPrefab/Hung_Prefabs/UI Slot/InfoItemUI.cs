
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InfoItemUI : MonoBehaviour
{
    public static InfoItemUI Instance;

    [SerializeField] private TextMeshProUGUI infoItemText;

    [Header("Action Button")]
    [SerializeField] private Button actionButton;
    [SerializeField] private TextMeshProUGUI actionButtonText;

    private ItemBase currentItem;
    private SlotUIType currentLocation;
    private void Awake()
    {
        Instance = this;
        Clear();
    }
    public void ShowInfo(ItemBase item, int amount, SlotUIType location)
    {
        if (item == null)
        {
            Clear();
            return;
        }
        currentItem = item;
        currentLocation = location;

        string rarityColor = GetColorByRarity(item.Rarity);

        infoItemText.text =
        $"<size=120%><color={rarityColor}><b>{item.name}</b></color></size>\n" +
        $"<color=grey>Type:</color> <color={rarityColor}>{item.Category}</color>\n" +
        $"<color=grey>Rarity:</color> <color={rarityColor}>{item.Rarity}</color>\n" +
        $"<color=grey>Amount:</color> {amount}\n\n" +
        $"<color=grey>Description:</color>\n<color={rarityColor}>{item.Description}</color>";
        // Hiển thị Stats của trang bị
        if (item.Stats != null && item.Stats.Count > 0)
        {
            infoItemText.text += "\n\n<color=grey>Stats:</color>\n";
            foreach(var stat in item.Stats)
            {
                infoItemText.text += $"- <color={rarityColor}>{stat.statType}: {stat.value}</color>\n";
            }
        }
        // Cập nhật nút hành động dựa trên vị trí
        SetupButton();
    }
    
    public void SetupButton()
    {
        actionButton.onClick.RemoveAllListeners();

        // Nếu không phải equipment thì ẩn button
        if (currentItem.Category != ItemCategory.Equipment)
        {
            actionButton.gameObject.SetActive(false);
            return;
        }

        actionButton.gameObject.SetActive(true);

        if (currentLocation == SlotUIType.Inventory)
        {
            actionButtonText.text = "Equip";
            actionButton.onClick.AddListener(OnClickEquip);
        }
        else if (currentLocation == SlotUIType.Equipment)
        {
            actionButtonText.text = "Unequip";
            actionButton.onClick.AddListener(OnClickUnequip);
        }
    }
    private void OnClickEquip()
    {
        Debug.Log("Equip: " + currentItem.name);

        

        Clear();
    }

    private void OnClickUnequip()
    {
        Debug.Log("Unequip: " + currentItem.name);

        

        Clear();
    }
    public void Clear()
    {
        infoItemText.text = "";
    }
    private string GetColorByRarity(Rarity rarity)
    {
        switch(rarity)
        {
            case Rarity.Common:
                return "White"; // White
            case Rarity.Uncommon:
                return "Green"; // Green
            case Rarity.Rare:
                return "Blue"; // Blue
            case Rarity.Epic:
                return "Purple"; // Purple
            case Rarity.Legendary:
                return "Orange"; // Orange
            default:
                return "White"; // Default to white
        }
    }
}
