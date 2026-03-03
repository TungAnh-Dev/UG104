using UnityEngine;
using UnityEngine.InputSystem;
public class TargetSelector : MonoBehaviour
{
    //Set layer cua target duoc chon
    [SerializeField] private LayerMask targetLayer;

    private Camera mainCamera;

    private TargetComponent currentTarget;

    public Transform CurrentTarget => currentTarget != null ? currentTarget.transform : null;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleSelect();
        }
    }

    private void HandleSelect()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, targetLayer))
        {
            TargetComponent newTarget = hit.collider.GetComponentInParent<TargetComponent>();

            if (newTarget == null)
                Debug.Log("Chua chon dung muc tieu");

            // Nếu click đúng target hiện tại thì không làm gì
            if (currentTarget == newTarget)
                return;
            // Bo chon muc tieu cu
            if (currentTarget != null)
                currentTarget.OnDeselected();

            currentTarget = newTarget;
            currentTarget.OnSelected();
            Debug.Log($"Đã chọn mục tiêu {currentTarget.name}");
        }
        else
        {
            ClearTarget();
        }
    }
    private void ClearTarget()
    {
        if (currentTarget == null)
            return;

        currentTarget.OnDeselected();
        currentTarget = null;
        Debug.Log("Xóa mục tiêu");
    }


}
