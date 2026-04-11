using UnityEngine;
using UnityEngine.InputSystem;
public class FemaleKnight : CharacterBase
{
    public static FemaleKnight Instance;
    private AnimationComponent animationComponent;
    private SkillSystem skillSystem;

    [SerializeField] private Transform weaponPoint;

    private GameObject currentWeapon;

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

    public void HandleEquipment(GameObject equipPrefab)
    {
        Debug.Log("Handling equipment change..." + equipPrefab);
        if (equipPrefab == null) return;

        // Xóa vũ khí cũ
        if (currentWeapon != null)
            Destroy(currentWeapon);

        // Tạo vũ khí mới
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
}
