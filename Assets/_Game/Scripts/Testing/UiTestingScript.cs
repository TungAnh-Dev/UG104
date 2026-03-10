using UnityEngine;

public class UiTestingScript : MonoBehaviour
{
    public void OpenUI()
    {
        var data = new ConfirmUIData();
        data.DescTxt = "Test";
        UIManager.Instance.OpenUI<ConfirmUI>(data);
    }
}
