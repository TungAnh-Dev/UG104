using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image fill;

    private Transform target;
    private HealthComponent health;
    private Camera cam;

    public void SetupHP(HealthComponent healthComponent)
    {
        health = healthComponent;
        target = health.GetTransform();
        cam = Camera.main;

        health.OnHealthChanged += UpdateBar;

        // Cập nhật lần đầu
        UpdateBar();

        gameObject.SetActive(true);
    }

    private void UpdateBar()
    {
        if (health == null) return;

        float current = health.GetCurrentHealth();
        Debug.Log($"Cập nhật HP: {current}/{health.GetMaxHP()}");
        float max = health.GetMaxHP();

        fill.fillAmount = current / max;
    }

    private void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 screenPos =
            cam.WorldToScreenPoint(target.position + Vector3.up * 2f);

        transform.position = screenPos;
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateBar;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}