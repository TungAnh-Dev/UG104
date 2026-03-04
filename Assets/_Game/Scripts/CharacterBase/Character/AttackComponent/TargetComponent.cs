using UnityEngine;

public class TargetComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Renderer rd;
    private Color originalColor;
    private void Awake()
    {
        rd = GetComponentInChildren<Renderer>();

        if (rd != null)
            originalColor = rd.material.GetColor("_BaseColor");
    }

    public void OnSelected()
    {
        if (rd != null)
            rd.material.SetColor("_BaseColor", Color.red);
    }

    public void OnDeselected()
    {
        if (rd != null)
            rd.material.SetColor("_BaseColor", originalColor);
    }

}
