using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    // Lấy stats HP từ StatsComponent
    private StatsComponent statsComponent;
    // Biến lưu trữ HP hiện tại
    public float CurrentHP { get; private set; }

    private void Awake()
    {
        statsComponent = GetComponent<StatsComponent>();
    }
    //Hàm khởi tạo giá trị HP hiện tại 
    private void Start()
    {
        if (statsComponent != null)
        {
            CurrentHP = GetMaxHP();
        }
       
    }
    //Hàm get giá trị HP từ StatsComponent
    public float GetMaxHP()
    {
        return statsComponent.GetStat(StatsType.MaxHP);
    }
    // Hàm nhân vật nhận sát thương
    public void TakeDamage(float damage)
    {
        CurrentHP = CurrentHP - damage;
        //Set giá trị Hp hiện tại không được nhỏ hơn 0
        // và lớn hơn giá trị MaxHP
        CurrentHP = Mathf.Clamp(CurrentHP, 0, GetMaxHP());
        if(CurrentHP <= 0)
        {
            Die();
        }
    }
    // Hàm nhân vật có hồi máu
    public void Heal(float healValue)
    {
        CurrentHP = CurrentHP + healValue;
        //Set giá trị Hp hiện tại không được nhỏ hơn 0
        // và lớn hơn giá trị MaxHP
        CurrentHP = Mathf.Clamp(CurrentHP, 0, GetMaxHP());
    }
    // Hàm nhân vật chết
    public void Die()
    {
        Debug.Log("Character is dead");
    }
}
