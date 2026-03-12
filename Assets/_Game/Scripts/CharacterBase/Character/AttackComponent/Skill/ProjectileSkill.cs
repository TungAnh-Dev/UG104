using UnityEngine;

public class ProjectileSkill  : BaseSkill
{
    private float speed;
    private float duration;
    private float damage;

    public override void Init(SkillData skillData)
    {
        base.Init(skillData);

        speed = data.speed;
        duration = data.duration;
        damage = data.damage;
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        if (data == null) return;

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.forward = direction;
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        Debug.Log("Moving");
    }
    private void OnTriggerEnter(Collider other)
    {
        HealthComponent health = other.GetComponent<HealthComponent>();

        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }

        
    }
}
