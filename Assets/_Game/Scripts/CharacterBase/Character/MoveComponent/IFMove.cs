using UnityEngine;

public interface IFMove
{
    void MoveTo(Vector3 position, float moveSpeed);

    void Stop();
}
