using UnityEngine;

public interface IMove
{
    void MoveTo(Vector3 direction);

    void Stop();
}
