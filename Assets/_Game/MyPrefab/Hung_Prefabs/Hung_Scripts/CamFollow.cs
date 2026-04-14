using UnityEngine;
using UnityEngine.InputSystem;
public class CamFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 4f;
    public float height = 2f;
    public float sensitivity = 0.15f;

    private float yaw;
    private float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (target == null || Mouse.current == null)
            return;

        // ✅ Chỉ xoay khi giữ chuột trái
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            yaw += mouseDelta.x * sensitivity;
            pitch -= mouseDelta.y * sensitivity;

            pitch = Mathf.Clamp(pitch, -30f, 60f);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + Vector3.up * height + offset;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
