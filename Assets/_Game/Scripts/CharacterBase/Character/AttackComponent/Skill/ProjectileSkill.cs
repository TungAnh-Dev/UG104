using UnityEngine;

public class ProjectileSkill : BaseSkill
{
    private float speed;
    private float duration;

    private Vector3 moveDirection;

    public override void Init(SkillData skillData, StatsComponent ownerStats)
    {
        base.Init(skillData, ownerStats);

        speed = skillData.speed;
        duration = skillData.duration;

        // Xác định hướng bay ban đầu
        if (target != null)
            moveDirection = (target.position - transform.position).normalized;
        else
            moveDirection = transform.forward;

        Destroy(gameObject, duration);
    }

    private void Update()
    {
        if (moveDirection == Vector3.zero) return;

        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bỏ qua owner
        if (ownerStats != null && other.transform == ownerStats.transform)
            return;

        HealthComponent health = other.GetComponent<HealthComponent>();
        if (health == null) return;

        float atk = ownerStats.GetStat(StatsType.Attack);
        float damage = atk * data.damageMultiplier;

        health.TakeDamage(damage);

        Destroy(gameObject);
    }

    public override void Use()
    {
        // Projectile tự bay trong Update nên không cần gì ở đây
    }
}