using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InputManager :  MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button inventoryBtn;

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }
    public void InventoryOpen()
    {
        inventoryPanel.SetActive(true);
        inventoryBtn.gameObject.SetActive(false);
    }
    public void InventoryClose()
    {
        inventoryPanel.SetActive(false);
        inventoryBtn.gameObject.SetActive(true);
    }
}
