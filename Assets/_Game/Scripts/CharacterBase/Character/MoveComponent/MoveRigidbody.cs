using UnityEngine;

public class MoveRigidbody : IFMove
{
    private Rigidbody rigidbody;


    //Hàm khởi tạo MoveRigidbody
    public MoveRigidbody(Rigidbody rigidbody, StatsComponent statsComponent)
    {
        this.rigidbody = rigidbody;
    }
    public void MoveTo(Vector3 inputDirection, float moveSpeed)
    {
        
        Vector3 velocity = inputDirection.normalized * moveSpeed;
        rigidbody.linearVelocity = new Vector3(
            velocity.x,
            rigidbody.linearVelocity.y,
            // góc nhìn topdown nên giữ nguyên vận tốc y

            velocity.z);
    }

    public void Stop()
    {
        rigidbody.linearVelocity = Vector3.zero;
    }
}
