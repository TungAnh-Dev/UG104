using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InputManager :  MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button inventoryBtn;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private GameObject joyStick;
    private void Start()
    {
        inventoryPanel.SetActive(false);
        skillPanel.SetActive(true);
        joyStick.gameObject.SetActive(true);
    }
    public void InventoryOpen()
    {
        inventoryPanel.SetActive(true);
        inventoryBtn.gameObject.SetActive(false);
        skillPanel.SetActive(false);
        joyStick.gameObject.SetActive(false);

    }
    public void InventoryClose()
    {
        inventoryPanel.SetActive(false);
        inventoryBtn.gameObject.SetActive(true);
        skillPanel.SetActive(true);
        joyStick.gameObject.SetActive(true);

    }
}
