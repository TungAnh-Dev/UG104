using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : CharacterBase
{
    // Them NavMeshAgent vao nhan vat
    private NavMeshAgent agent;

    private Transform player;
    //Khoi tao cac chuc nang cua nhan vat
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        moveComponent.SetMoveComponent(new MoveNavMesh(agent, statsComponent));
    }
    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (!IsAlive || player == null) return;

        moveComponent.MoveTo(player.position);
    }
}
