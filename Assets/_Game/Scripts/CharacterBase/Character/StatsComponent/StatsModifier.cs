using UnityEngine;

public class StatsModifier
{
    // Kieu thuoc tinh se bi thay doi
    public StatsType StatType { get; private set; }
    // Gia tri cong them vao thuoc tinh
    public float Value { get; private set; }
    //Kieu cong them vao thuoc tinh: phan tram hay la gia tri
    public ModifierType Type { get; private set; }

    //Hàm khởi tạo để modifier thuộc tính cụ thể
    public StatsModifier(StatsType statType, float value, ModifierType type)
    {
        StatType = statType;
        Value = value;
        Type = type;
    }
}
