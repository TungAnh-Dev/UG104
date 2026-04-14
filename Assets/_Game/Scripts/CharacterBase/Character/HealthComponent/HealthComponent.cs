using UnityEngine;
using System.Collections;
public class HealthComponent : MonoBehaviour
{
    
    
    // Lấy stats HP từ StatsComponent
    private StatsComponent statsComponent;
    // Biến lưu trữ HP hiện tại
    public float CurrentHP { get; private set; }

    private TargetComponent targetComponent;
    public System.Action OnHealthChanged;
    private void Awake()
    {

            statsComponent = GetComponent<StatsComponent>();
            targetComponent = GetComponent<TargetComponent>();
   
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
    public float GetCurrentHealth()
    {
        return CurrentHP;
    }
    // Hàm nhân vật nhận sát thương
    public void TakeDamage(float damage)
    {
        if (CurrentHP <= 0) return;

        damage = Mathf.Max(0, damage);
        OnHealthChanged?.Invoke();
        //Khi nhân vật nhận damage
        CurrentHP -= damage;
        //Hp sẽ được giới hạn 
        CurrentHP = Mathf.Clamp(CurrentHP, 0,GetMaxHP());

        Debug.Log($"{gameObject.name} còn {CurrentHP}/{GetMaxHP()} HP");

        StartCoroutine(HitEffect());

        if (CurrentHP == 0) 
        {
            Die();
        }
    }
    private IEnumerator HitEffect()
    {
        targetComponent.OnSelected(); // đỏ

        yield return new WaitForSeconds(0.5f);

        targetComponent.OnDeselected(); // về màu cũ
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
