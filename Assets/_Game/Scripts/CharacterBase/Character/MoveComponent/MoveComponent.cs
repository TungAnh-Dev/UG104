using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private IFMove ifMove;
    //Thêm component StateComponent để lấy được trạng thái của nhân vật
    private StatsComponent statsComponent;

    private void Awake()
    {
        statsComponent = GetComponent<StatsComponent>();
    }
    //Set MoveComponent theo loại di chuyển của nhân vật
    public void SetMoveComponent(IFMove moveType)
    {
        ifMove = moveType;
    }
    //Hàm di chuyển của nhân vật theo hướng di chuyển
    public void MoveTo(Vector3 direction)
    {
        if (ifMove == null) return;

        //Lấy vận tốc di chuyển từ StatsComponent
        float moveSpeed = statsComponent.GetStat(StatsType.MoveSpeed);
        // Gọi hàm di chuyển của IFMove 
        ifMove.MoveTo(direction, moveSpeed);
    }

    public void Stop()
    {
        ifMove?.Stop();
    }
}