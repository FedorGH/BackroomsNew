using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject draggedObj;
    InventoryManager inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedObj = gameObject;
        inventory.dragPrefab.SetActive(true);
        inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = false;

        if(draggedObj.GetComponent<CurrentItem>() != null)
        {
            int index = draggedObj.GetComponent<CurrentItem>().index;
            inventory.dragPrefab.GetComponent<Image>().sprite = inventory.items[index].iconItem;
            
            if (inventory.items[index].countItem > 1)
            {
                inventory.dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = inventory.items[index].countItem.ToString();
            }
            else
            {
                inventory.dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = null;
            }

            if (inventory.dragPrefab.GetComponent<Image>().sprite == null)
            {
                draggedObj = null;
                inventory.dragPrefab.SetActive(false);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventory.dragPrefab.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            draggedObj.GetComponent<CurrentItem>().Drop();
            draggedObj.GetComponent<CurrentItem>().Remove();
        }

        draggedObj = null;
        inventory.dragPrefab.SetActive(false);
        inventory.dragPrefab.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

//if (Input.GetMouseButton(1))
//{
//    if (inventory.items[index].countItem > 1)
//    {
//        inventory.dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = inventory.items[index].countItem.ToString();
//    }
//    else
//    {
//        inventory.dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = null;
//    }
//}
//else
//{
//    inventory.dragPrefab.transform.GetChild(0).GetComponent<TMP_Text>().text = null;
//}
