using UnityEngine;

public class Enemie : CharacterBase
{
    private Transform target;
    private MoveComponent moveComponent;

    private void Start()
    {
        moveComponent = GetComponent<MoveComponent>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    private void Update()
    {
        if (target == null) return;
        Debug.Log($"Enemy is moving towards: {target.position}");
        moveComponent.MoveTo(target.position);
    }
}