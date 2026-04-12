using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;

    private string skillName;
    private SkillSystem skillSystem;

    private void Awake()
    {
        button.gameObject.SetActive(false);
    }

    public void Init(SkillSystem system)
    {
        skillSystem = system;
    }

    public void SetSkill(SkillData data)
    {
        if (data == null)
        {
            button.gameObject.SetActive(false);
            icon.enabled = false;
            return;
        }

        button.gameObject.SetActive(true);
        icon.enabled = true;
        icon.sprite = data.iconSkill;
    }

    public void OnClick()
    {
        Debug.Log("Button được bấm");

        if (skillSystem == null)
        {
            Debug.LogWarning("skillSystem null!");
            return;
        }

        skillSystem.UseCurrentSkill();

    }
}