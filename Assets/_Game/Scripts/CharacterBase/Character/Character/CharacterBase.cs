
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //Các thuộc tính cơ bản của nhân vật trong game
    
    
    [Header("Character Components")]
    protected HealthComponent healthComponent;
    protected StatsComponent statsComponent;
    protected MoveComponent moveComponent;
    protected TargetSelector targetSelector;
    

    //Set các component bởi Awake
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        healthComponent = GetComponent<HealthComponent>();
        statsComponent = GetComponent<StatsComponent>();
        moveComponent = GetComponent<MoveComponent>();

        targetSelector = GetComponent<TargetSelector>();
        
    }
}
