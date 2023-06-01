using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Active : MonoBehaviour
{
    [SerializeField] GameObject inventory_panel;
    private bool inventoryOpen = false;

    void Start()
    {
        inventory_panel.SetActive(false);
    }

    void Update()
    {
        Inventory_SetActive();
    }

    public void Inventory_SetActive()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // При нажатии клавиши "I" открываем/закрываем инвентарь
            inventoryOpen = !inventoryOpen;
            inventory_panel.SetActive(inventoryOpen);
        }
    }
}
