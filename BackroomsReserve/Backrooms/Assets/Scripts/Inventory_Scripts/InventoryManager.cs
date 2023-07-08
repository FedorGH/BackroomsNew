using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instanceInventory;

    [HideInInspector] public List<Item> items;
    public FavoriteManager favorite;
    public GameObject inventoryPanel;
    public GameObject parentCells;
    public GameObject player;
    public GameObject dataBase;
    public GameObject dragPrefab;

    [Header("Button assignment")]
    public KeyCode showInventory;
    public KeyCode takeObj;

    [Header("Massage")]
    public GameObject massageManager; //оповещение о добавлении предмета в инвентарь
    public GameObject massage;

    void Awake()
    {
        instanceInventory = this;
    }

    // Use this for initialization
    void Start()
    {
        InitInventory();
        DisplayItems();
        NumItems();

        RenameCells();
        RenameIcons();
        HidePanel();

    }

    void InitInventory()
    {
        items = new List<Item>();
        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            items.Add(EmptySlot());
        }
    }

    public Item EmptySlot()
    {
        return gameObject.AddComponent<Item>();
    }

    public void AddUnStackableItem(Item currentItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == EmptySlotID())
            {
                items[i] = currentItem;
                items[i].countItem = 1;
                DisplayItems();
                //Destroy(currentItem.gameObject);
                break;
            }
        }
    }

    void Massage(Item currentItem)
    {
        GameObject massObj = Instantiate(massage);
        massObj.transform.SetParent(massageManager.transform);

        TMP_Text textMsg = massObj.transform.GetChild(1).GetComponent<TMP_Text>(); // текст в оповещении о подборе предмета
        textMsg.text = currentItem.itemName;

        Image iconMsg = massObj.transform.GetChild(0).GetComponent<Image>(); // картинка в оповещении при подборе предмета
        iconMsg.sprite = currentItem.iconItem;
    }

    public int EmptySlotID()
    {
        return 0;
    }

    void AddStackableItem(Item currentItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == currentItem.id)
            {
                items[i].countItem++;
                DisplayItems();
                //Destroy(currentItem.gameObject);
                return;
            }
        }
        AddUnStackableItem(currentItem);
    }

    public void AddItem(Item currentItem)
    {
        if (currentItem.isStackable)
            AddStackableItem(currentItem);
        else
            AddUnStackableItem(currentItem);
    }

    void NumItems()
    {
        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            Transform cell = parentCells.transform.GetChild(i);
            cell.GetComponent<CurrentItem>().ItemNum = i;
        }
    }

    void RenameCells()
    {
        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            Transform cell = parentCells.transform.GetChild(i);
            cell.name = "Cell " + i.ToString();
        }
    }

    void RenameIcons()
    {
        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            Transform cell = parentCells.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            icon.name = "Icon " + i.ToString();
        }
    }

    void Update()
    {
        TakeItem();
        SwitchPanel();
    }

    void TakeItem()
    {
        if (Input.GetKeyDown(takeObj))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3f))
            {
                if (hit.collider.GetComponent<Item>())
                {
                    Item currentItem = hit.collider.GetComponent<Item>();
                    Massage(currentItem);
                    AddItem(currentItem);
                    Destroy(currentItem.gameObject);
                }
            }
        }
    }

    void SwitchPanel()
    {
        if (Input.GetKeyDown(showInventory))
        {
            if (inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(false);
                dragPrefab.SetActive(false);
                player.GetComponent<PlayerCont>().enabled = true;
                player.GetComponent<CrosshairCont>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                inventoryPanel.SetActive(true);
                player.GetComponent<PlayerCont>().enabled = false;
                player.GetComponent<CrosshairCont>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void HidePanel()
    {
        inventoryPanel.SetActive(false);
    }

    public void DisplayItems()
    {

        for (int i = 0; i < parentCells.transform.childCount; i++)
        {
            Item currentItem = items[i];
            Transform cell = parentCells.transform.GetChild(i);

            Image icon = cell.transform.GetChild(0).GetComponent<Image>();

            TMP_Text count = icon.transform.GetChild(0).GetComponent<TMP_Text>();

            if (currentItem.id != EmptySlotID())
            {
                icon.enabled = true;
                Sprite itemIcon = currentItem.iconItem;
                icon.sprite = itemIcon;
                count.text = null;

                if (currentItem.isStackable)
                {
                    if (currentItem.countItem > 1)
                    {
                        count.text = currentItem.countItem.ToString();
                    }
                    else
                    {
                        count.text = null;
                    }
                }
            }
            else
            {
                icon.enabled = false;
                icon.sprite = null;
                count.text = null;
            }
        }
    }

    public void RemoveItem(int numItem)
    {
        if (items[numItem].countItem > 1)
        {
            items[numItem].countItem--;
        }
        else
        {
            items[numItem] = EmptySlot();
        }
        DisplayItems();
    }

    public void DroppedItem(int id)
    {
        for (int i = 0; i < dataBase.transform.childCount; i++)
        {
            Item item = dataBase.transform.GetChild(i).GetComponent<Item>();
            if (item != null)
            {
                if (item.id == id)
                {
                    GameObject obj = Instantiate(item.gameObject);
                    obj.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                }
            }
        }
    }
    public bool IsExistItem(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemID(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
            {
                if (items[i].countItem > 1)
                {
                    items[i].countItem--;
                }
                else
                {
                    items[i] = EmptySlot();
                }
                DisplayItems();
            }
        }
    }
}
