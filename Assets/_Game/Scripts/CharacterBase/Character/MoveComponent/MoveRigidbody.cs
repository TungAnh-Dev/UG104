using UnityEngine;

public class MoveRigidbody : MonoBehaviour, IMove
{
    private Rigidbody rb;

    private StatsComponent stats;

    private Vector3 moveDirection;
    private bool isMoving;
    //Hàm khởi tạo MoveRigidbody
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<StatsComponent>();
    }

    public void MoveTo(Vector3 direction)
    {
        
        moveDirection = direction.normalized;
        isMoving = true;
    }

    public void Stop()
    {
        isMoving = false;
        rb.linearVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (!isMoving) return;
        //Lấy moveSpeed runtime từ stats
        float moveSpeed = stats.GetStat(StatsType.MoveSpeed);

        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
