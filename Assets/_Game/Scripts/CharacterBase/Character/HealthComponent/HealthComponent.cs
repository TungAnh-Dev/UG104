using UnityEngine;

public class HealthComponent : MonoBehaviour, IAttackable
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
    public Transform GetTransform()
    {
        return transform;
    }
    //Hàm get giá trị HP từ StatsComponent
    public float GetMaxHP()
    {
        return statsComponent.GetStat(StatsType.MaxHP);
    }
    // Hàm nhân vật nhận sát thương
    public void TakeDamage(float damage)
    {
        if (CurrentHP <= 0) return;

        damage = Mathf.Max(0, damage);

        //Khi nhân vật nhận damage
        CurrentHP -= damage;
        //Hp sẽ được giới hạn 
        CurrentHP = Mathf.Clamp(CurrentHP, 0,GetMaxHP());

        Debug.Log($"{gameObject.name} còn {CurrentHP}/{GetMaxHP()} HP");

        if (CurrentHP == 0) 
        {
            Die();
        }
    }
    // Hàm nhân vật có hồi máu
    public void Heal(float healValue)
    {
        
    }
    // Hàm nhân vật chết
    public void Die()
    {
        Debug.Log($"{gameObject.name} + died");
        Destroy(gameObject);
    }
}
