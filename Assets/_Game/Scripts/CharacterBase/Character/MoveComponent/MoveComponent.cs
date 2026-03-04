using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private IMove iMove;

    //Set MoveComponent theo loại di chuyển của nhân vật
    private void Awake()
    {
        //Tìm component implement IMove
        iMove = GetComponent<IMove>();
        if(iMove == null)
        {
            Debug.LogWarning($"{gameObject.name} không có component IMove");
        }
    }
    //Hàm di chuyển của nhân vật theo hướng di chuyển
    public void MoveTo(Vector3 direction)
    {
        iMove?.MoveTo(direction);
    }

    public void Stop()
    {
        iMove?.Stop();
    }
}