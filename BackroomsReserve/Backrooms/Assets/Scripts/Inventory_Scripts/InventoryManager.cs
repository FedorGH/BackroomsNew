using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector] public List<InventoryItem> items;
    public GameObject parentCells;
    public GameObject player;
    public GameObject dataBase;
    public GameObject dragPrefab;

    bool a;

    [Header("Button assignment")]
    public KeyCode showInventory;
    public KeyCode takeObj;

    [Header("Massage")]
    public GameObject massageManager; //оповещение о добавлении предмета в инвентарь
    public GameObject massage;

    private void Start()
    {
        items = new List<InventoryItem>();

        parentCells.SetActive(false);

        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            parentCells.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
        }

        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            items.Add(gameObject.AddComponent<InventoryItem>());
        }
    }
    private void Update()
    {
        InventoryActive();

        if (Input.GetKeyDown(takeObj))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                if (hit.collider.GetComponent<InventoryItem>())
                {
                    InventoryItem currentItem = hit.collider.GetComponent<InventoryItem>();
                    Massage(currentItem);
                    AddItem(currentItem);
                }
            }
        }
    }

    void Massage(InventoryItem currentItem)
    {
        GameObject massObj = Instantiate(massage);
        massObj.transform.SetParent(massageManager.transform);

        TMP_Text textMsg = massObj.transform.GetChild(1).GetComponent<TMP_Text>(); // текст в оповещении о подборе предмета
        textMsg.text = currentItem.itemName;

        Image iconMsg = massObj.transform.GetChild(0).GetComponent<Image>(); // картинка в оповещении при подборе предмета
        iconMsg.sprite = currentItem.iconItem;
    }

    void AddItem(InventoryItem currentItem)
    {
        if (currentItem.isStackable)
        {
            AddStackableItem(currentItem);
        }
        else
        {
            AddUnstackableItem(currentItem);
        }
    }

    void AddUnstackableItem(InventoryItem currentItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == 0)
            {
                items[i] = currentItem;
                DisplayItems();
                Destroy(currentItem.gameObject);
                break;
            }
        }
    }

    void AddStackableItem(InventoryItem currentItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == currentItem.id)
            {
                items[i].countItem++;
                DisplayItems();
                Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnstackableItem(currentItem);
    }

    void InventoryActive()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (parentCells.activeSelf)
            {
                parentCells.SetActive(false);
                dragPrefab.SetActive(false);
                player.GetComponent<PlayerCont>().enabled = true;
                player.GetComponent<CrosshairCont>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                parentCells.SetActive(true);
                player.GetComponent<PlayerCont>().enabled = false;
                player.GetComponent<CrosshairCont>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void DisplayItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Transform cell = parentCells.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform stacItem = icon.GetChild(0);

            TMP_Text text = stacItem.GetComponent<TMP_Text>();
            Image img = icon.GetComponent<Image>();
            
            if (items[i].id != 0)
            {
                img.enabled = true;
                img.sprite = items[i].iconItem;

                if(items[i].countItem > 1)
                {
                    text.text = items[i].countItem.ToString();
                }
                else
                {
                    text.text = null;
                }
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
                text.text = null;
            }
        }
    }
}