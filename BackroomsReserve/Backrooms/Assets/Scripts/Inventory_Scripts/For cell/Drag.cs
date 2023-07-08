using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject isDraggingObject;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject dragPrefab = InventoryManager.instanceInventory.dragPrefab;
        isDraggingObject = gameObject;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().enabled = true;

            if (isDraggingObject.GetComponent<CurrentItem>())
            {
                dragPrefab.GetComponent<Image>().sprite = GetComponent<CurrentItem>().CurrentInventoryItem.iconItem;
                if (GetComponent<CurrentItem>().CurrentInventoryItem.countItem > 1)
                    dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = GetComponent<CurrentItem>().CurrentInventoryItem.countItem.ToString();
                else
                    dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().enabled = false;
            }

            if (dragPrefab.GetComponent<Image>().sprite != null)
            {
                dragPrefab.SetActive(true);
                dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                isDraggingObject = null;
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().enabled = false;

            if (isDraggingObject.GetComponent<CurrentItem>())
            {
                dragPrefab.GetComponent<Image>().sprite = GetComponent<CurrentItem>().CurrentInventoryItem.iconItem;
            }

            if (dragPrefab.GetComponent<Image>().sprite != null)
            {
                dragPrefab.SetActive(true);
                dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            else
            {
                isDraggingObject = null;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.instanceInventory.dragPrefab.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraggingObject = null;
        GameObject dragPrefab = InventoryManager.instanceInventory.dragPrefab;
        dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
        dragPrefab.GetComponent<Image>().sprite = null;
        dragPrefab.SetActive(false);
    }

    public InventoryItem dragedItem(InventoryItem item)
    {
        for (int i = 0; i < InventoryManager.instanceInventory.dataBase.transform.childCount; i++)
        {
            InventoryItem temp = InventoryManager.instanceInventory.dataBase.transform.GetChild(i).GetComponent<InventoryItem>();
            if (temp.id == item.id)
            {
                return temp;
            }
        }
        return null;
    }

    public void AddItem(InventoryItem item)
    {
        item.countItem++;
    }
}
