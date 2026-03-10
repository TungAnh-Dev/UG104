using UnityEngine;
using UnityEngine.InputSystem;

public class KnightMale : CharacterBase
{
    private ModifierController modifierController;
    private StatsModifier speedbuff;
    private AnimationComponent animationComponent;


    public override void Init()
    {
            base.Init();
    }
    //
    private void Start()
    {
        modifierController = GetComponent<ModifierController>();
        animationComponent = GetComponent<AnimationComponent>();

        speedbuff = new StatsModifier(
            StatsType.MoveSpeed,
            0,
            ModifierType.Flat);
        modifierController.AddModifier(speedbuff);
    }
    private void Update()
    {
        HandleMovement();
        HandleModifier();
    }

    public void HandleModifier()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            speedbuff.AddValue(2f);
            Debug.Log("Increase Speed");
        }
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            speedbuff.AddValue(-2f);
            Debug.Log("Decrease Speed");
        }
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
            moveComponent.MoveTo(direction.normalized);
            animationComponent.SetRun();
            //Xoay theo huong di chuyen

            transform.rotation = Quaternion.LookRotation(direction);

        }
        else
        {
            moveComponent.Stop();
            animationComponent.SetIdle();

        }
    }

    
}
