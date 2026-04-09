using UnityEngine;
using UnityEngine.InputSystem;
public class CameraPreview : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.4f, 2.5f);
    [SerializeField] private float rotateSpeed = 2f;

    [SerializeField] private RectTransform centerPanel;
    private float currentY;

    private void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.LookAt(target.position + Vector3.up * 1.3f);

        // Không có chuột được nhấn thì bỏ qua
        if (Mouse.current == null)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        // Kiểm tra chuột có nằm trong CenterPanel không
        if (!RectTransformUtility.RectangleContainsScreenPoint(centerPanel, mousePos))
        {
            return;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            float mouseX = Mouse.current.delta.ReadValue().x;
            currentY += -mouseX * rotateSpeed;
            
        }
        // Tính vị trí camera xoay quanh target
        Quaternion rotation = Quaternion.Euler(0f, currentY, 0f);
        Vector3 rotatedOffset = rotation * offset;

        transform.position = target.position + rotatedOffset;
        transform.LookAt(target.position + Vector3.up * 1.3f);
    }
}
