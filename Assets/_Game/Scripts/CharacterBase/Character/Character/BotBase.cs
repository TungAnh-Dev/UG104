using UnityEngine;

public abstract class BotBase : MonoBehaviour
{
    protected BotState currentState;

    protected virtual void Start()
    {
        currentState = BotState.Idle;
    }

    protected virtual void Update()
    {
        if (currentState == BotState.Dead) return;
        UpdateState();
    }

    protected virtual void UpdateState()
    {
        switch (currentState)
        {
            case BotState.Idle:
                OnIdle();
                break;

            case BotState.Move:
                OnMove();
                break;

            case BotState.Attack:
                OnAttack();
                break;
        }
    }

    protected virtual void OnIdle() { }
    protected virtual void OnMove() { }
    protected virtual void OnAttack() { }

    protected void ChangeState(BotState newState)
    {
        currentState = newState;
    }

    protected abstract void OnDeath();
}