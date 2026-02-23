using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : CharacterBase
{
    //them rigidbody vao nhan vat
    private Rigidbody rb;
    //Khoi tao cac chuc nang cua nhan vat
    protected override void Awake()
    {
        base.Awake();
 
        rb = GetComponent<Rigidbody>();

        moveComponent.SetMoveComponent(new MoveRigidbody(rb, statsComponent));
    }

    private void FixedUpdate()
    {
        if (!IsAlive) return;
        //Test di chuyen cua nhan vat
        HandleMove();
    }

    private void HandleMove()
    {
        if (!IsAlive) return;
        //Tao vecto huong cho nhan vat
        Vector3 direction = Vector3.zero;

        if (Keyboard.current.dKey.isPressed)
        {
            Debug.Log("Turn right");
            direction += Vector3.right;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            Debug.Log("Turn left");
            direction += Vector3.left;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            Debug.Log("Go foward");
            direction += Vector3.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            Debug.Log("Go Back");
            direction += Vector3.back;
        }
        if (direction != Vector3.zero)
        {
            moveComponent.MoveTo(direction.normalized);
        }
        else
        {
            moveComponent.Stop();
        }
    }
}