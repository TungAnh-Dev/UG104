using UnityEngine;
using UnityEngine.AI;
public class PlayerNavMeshMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    [Header("Range Settings")]
    [SerializeField] private float detectRange = 10f;   // Phạm vi phát hiện
    [SerializeField] private float stopDistance = 2f;   // Khoảng cách dừng lại

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stopDistance;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // Nếu Player trong phạm vi phát hiện
        if (distance <= detectRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }
}