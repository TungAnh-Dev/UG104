using UnityEngine;
using UnityEngine.InputSystem;
public class FemaleKnight : CharacterBase
{
    private AnimationComponent animationComponent;
    public override void Init()
    {
        base.Init();
    }

    public void Start()
    {
        animationComponent = GetComponent<AnimationComponent>();
    }

    public void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        Vector3 direction = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
            direction += Vector3.forward;

        if (Keyboard.current.sKey.isPressed)
            direction += Vector3.back;

        if (Keyboard.current.aKey.isPressed)
            direction += Vector3.left;

        if (Keyboard.current.dKey.isPressed)
            direction += Vector3.right;

        if (direction != Vector3.zero)
        {
            moveComponent.MoveTo(direction);
            animationComponent.PlayMove();

            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            moveComponent.Stop();
            animationComponent.PlayIdle();
        }
    }
}
