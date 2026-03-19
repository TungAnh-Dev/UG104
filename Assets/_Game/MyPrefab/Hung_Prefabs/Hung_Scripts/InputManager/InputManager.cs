using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager :  MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    private bool isInventoryOpen = false;
    private void Start()
    {
        inventoryPanel.SetActive(isInventoryOpen);
    }
    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            InventoryOpen();
        }
    }

    private void InventoryOpen()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
    }
}
