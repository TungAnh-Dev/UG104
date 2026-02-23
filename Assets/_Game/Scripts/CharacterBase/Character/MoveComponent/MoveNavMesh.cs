using UnityEngine;
using UnityEngine.AI;
public class MoveNavMesh : IFMove
{
    private NavMeshAgent agent;

    //Hàm khởi tạo MoveNavMesh
    public MoveNavMesh(NavMeshAgent agent, StatsComponent statsComponent)
    {
        this.agent = agent;
    }

    public void MoveTo(Vector3 position, float moveSpeed)
    {
        if(agent == null) return;

        agent.speed = moveSpeed;

        if(agent.isStopped)
        {
            agent.isStopped = false;
        }
        // Khi ma khong co duong di moi 
        if (!agent.hasPath ||
            (agent.destination - position).sqrMagnitude > 0.1f)
        {
            agent.SetDestination(position);
        }
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }
}
