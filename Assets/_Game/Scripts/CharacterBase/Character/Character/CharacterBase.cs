using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //Các thuộc tính cơ bản của nhân vật trong game
    [Header("Character Stats")]
    public string characterName;
    public int level = 1;
    
    [Header("Character Components")]
    protected HealthComponent healthComponent;
    protected StatsComponent statsComponent;
    protected MoveComponent moveComponent;

    [SerializeField]
    private float baseMoveSpeed = 5f;

    //Dùng bool IsAlive để kiểm tra nhân vật còn sống hay không
    protected bool isAlive = true;
    // 
    public bool IsAlive
    {
        get 
        {
            return isAlive;
        }
    }
    //Set các component bởi Awake
    protected virtual void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        statsComponent = GetComponent<StatsComponent>();
        moveComponent = GetComponent<MoveComponent>();

        statsComponent.SetBaseStats(StatsType.MoveSpeed, baseMoveSpeed);
    }

    // Nếu như nhân vật chết
    public virtual void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        
        moveComponent?.Stop();
    }
}
