using UnityEngine;
using UnityEngine.InputSystem;

public class FemaleKnight : CharacterBase
{
    public static FemaleKnight Instance;

    private AnimationComponent animationComponent;
    private SkillSystem skillSystem;

    public Joystick joystick;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform weaponPoint;
    private GameObject currentWeapon;

    private Vector3 moveDirection;

    public override void Init()
    {
        base.Init();
        Instance = this;
    }

    public void Start()
    {
        animationComponent = GetComponent<AnimationComponent>();
        skillSystem = GetComponent<SkillSystem>();
    }

    public void Update()
    {
        GetInput();
        MoveCharacter();
        TestTakeDamage();
    }
    void GetInput()
    {
        Vector2 keyboard = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) keyboard.y += 1;
            if (Keyboard.current.sKey.isPressed) keyboard.y -= 1;
            if (Keyboard.current.aKey.isPressed) keyboard.x -= 1;
            if (Keyboard.current.dKey.isPressed) keyboard.x += 1;
        }

        Vector2 joystickInput = Vector2.zero;

        if (joystick != null)
            joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        Vector2 input = keyboard + joystickInput;

        if (input.magnitude > 1)
            input.Normalize();

        moveDirection = new Vector3(input.x, 0f, input.y);
    }

    // ===== MOVE =====
    void MoveCharacter()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            // Lấy hướng camera (bỏ Y để không bị nghiêng)
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            // Tính hướng di chuyển theo camera
            Vector3 move = camForward * moveDirection.z +
                           camRight * moveDirection.x;

            move.Normalize();

            // Di chuyển
            moveComponent.MoveTo(move);
            animationComponent.PlayMove();

            // Xoay mượt theo hướng di chuyển
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                100f * Time.deltaTime
            );
        }
        else
        {
            moveComponent.Stop();
            animationComponent.PlayIdle();
        }
    }

    public void HandleEquipment(GameObject equipPrefab)
    {
        Debug.Log("Handling equipment change..." + equipPrefab);

        if (equipPrefab == null) return;

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(equipPrefab, weaponPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }

    public void HandleUnequipment()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }
    }

    private void TestTakeDamage()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            GetComponent<HealthComponent>().TakeDamage(10);
        }
    }
}