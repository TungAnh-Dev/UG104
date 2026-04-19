using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HealthBarUI healthBarPrefab;
    [SerializeField] private Canvas targetCanvas;

    private HealthBarUI healthBar;
    private HealthComponent health;

    private void Awake()
    {
        health = GetComponent<HealthComponent>();

        if (health == null)
        {
            Debug.LogError("Enemy thiếu HealthComponent!");
        }
    }

    private void Start()
    {
        if (healthBarPrefab == null || targetCanvas == null)
        {
            Debug.LogError("Chưa gán HealthBarPrefab hoặc Canvas!");
            return;
        }

        healthBar = Instantiate(healthBarPrefab, targetCanvas.transform);

        // ✅ Truyền HealthComponent chứ không phải Transform
        healthBar.SetupHP(health);
    }

    private void OnDestroy()
    {
        if (healthBar != null)
            Destroy(healthBar.gameObject);
    }
}