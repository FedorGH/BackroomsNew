using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CurrentItem : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    [HideInInspector] public int index;

    GameObject inventoryObj;
    InventoryManager inventory;

    void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("InventoryManager");
        inventory = inventoryObj.GetComponent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(inventory.items[index].customEvent != null)
            {
                inventory.items[index].customEvent.Invoke();
            }

            if (inventory.items[index].forFood)
            {
                Remove();
            }
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (inventory.items[index].isDropped)
            {
                Drop();
                Remove();
            }
        }
    }
    
    public void Remove()
    {
        if (inventory.items[index].id != 0)
        {
            if (inventory.items[index].countItem > 1)
            {
                inventory.items[index].countItem--;
            }
            else
            {
                inventory.items[index] = gameObject.AddComponent<InventoryItem>();
            }

            inventory.DisplayItems();
        }
    }

    public void Drop()
    {
        if (inventory.items[index].id != 0)
        {
            for (int i = 0; i < inventory.dataBase.transform.childCount; i++)
            {
                InventoryItem item = inventory.dataBase.transform.GetChild(i).GetComponent<InventoryItem>();
                if (item) 
                {
                    if (inventory.items[index].id == item.id)
                    {
                        GameObject droppedObj = Instantiate(item.gameObject);
                        droppedObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                    }
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //int x = 3;
        //int y = 5;

        //int z = x;
        //x = y;
        //y = z;

        GameObject dragObj = Drag.draggedObj;
        if(dragObj == null)
        {
            return;  //если ячейка пустая, то перезапускаем метод
        }
        CurrentItem currentDragItem = dragObj.GetComponent<CurrentItem>();
        if (currentDragItem)
        {
            InventoryItem currentItem = inventory.items[GetComponent<CurrentItem>().index];
            inventory.items[GetComponent<CurrentItem>().index] = inventory.items[currentDragItem.index];
            inventory.items[currentDragItem.index] = currentItem;
            inventory.DisplayItems();
        }
    }
}
