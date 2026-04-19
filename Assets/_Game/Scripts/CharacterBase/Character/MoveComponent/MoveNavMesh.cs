using UnityEngine;
using UnityEngine.AI;

public class MoveNavMesh : MonoBehaviour, IMove
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (agent == null) return;

        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }

    public void Stop()
    {
        if (agent == null) return;

        agent.isStopped = true;
        agent.ResetPath();
    }
}