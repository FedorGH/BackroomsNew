using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FavoriteManager : MonoBehaviour
{
    [HideInInspector]
    public List<Item> favoriteItem;

    void Start()
    {
        InitFavourite();
        DisplayItems();
        NumItems();
    }

    void InitFavourite()
    {
        favoriteItem = new List<Item>();
        for (int i = 0; i < transform.childCount; i++)
        {
            favoriteItem.Add(gameObject.AddComponent<Item>());
        }
    }

    void NumItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform cell = transform.GetChild(i);
            cell.GetComponent<FavoriteItem>().ItemNum = i;
        }
    }

    public void DisplayItems()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Item currentItem = favoriteItem[i];
            Transform cell = transform.GetChild(i);

            Image icon = cell.transform.GetChild(0).GetComponent<Image>();
            TMP_Text count = icon.transform.GetChild(0).GetComponent<TMP_Text>();

            if (currentItem.id != 0)
            {

                icon.enabled = true;
                Sprite itemIcon = currentItem.iconItem;
                icon.sprite = itemIcon;
                count.text = null;

                if (currentItem.isStackable)
                {
                    if (currentItem.countItem > 1)
                        count.text = currentItem.countItem.ToString();
                    else
                        count.text = null;
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
        if (favoriteItem[numItem].countItem > 1)
            favoriteItem[numItem].countItem--;
        else
            favoriteItem[numItem] = InventoryManager.instanceInventory.EmptySlot();

        DisplayItems();
    }

    public bool IsExistItem(int id)
    {
        for (int i = 0; i < favoriteItem.Count; i++)
        {
            if (favoriteItem[i].id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItemID(int id)
    {
        for (int i = 0; i < favoriteItem.Count; i++)
        {
            if (favoriteItem[i].id == id)
            {
                if (favoriteItem[i].countItem > 1)
                    favoriteItem[i].countItem--;
                else
                    favoriteItem[i] = InventoryManager.instanceInventory.EmptySlot();
                DisplayItems();
            }
        }
    }
}
