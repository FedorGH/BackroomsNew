using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class CurrentItem : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    private int itemNum;

    public Item CurrentInventoryItem
    {
        get { return InventoryManager.instanceInventory.items[ItemNum]; }
        set { InventoryManager.instanceInventory.items[ItemNum] = value; }
    }

    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Drag.isDraggingObject != gameObject)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (CurrentInventoryItem.customEvent != null)
                {
                    CurrentInventoryItem.customEvent.Invoke();
                }
                if (CurrentInventoryItem.forFood)
                {
                    InventoryManager.instanceInventory.RemoveItem(ItemNum);
                }
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (CurrentInventoryItem.isDropped)
                {
                    InventoryManager.instanceInventory.DroppedItem(CurrentInventoryItem.id);
                    InventoryManager.instanceInventory.RemoveItem(ItemNum);
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragedObject = Drag.isDraggingObject;

        if (dragedObject == null || dragedObject == gameObject)
        {
            return;
        }

        CurrentItem dragedCurrentItem = dragedObject.GetComponent<CurrentItem>();
        FavoriteItem dragedFavouriteItem = dragedObject.GetComponent<FavoriteItem>();

        Drag drag = Drag.isDraggingObject.GetComponent<Drag>();

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (dragedObject.GetComponent<CurrentItem>())
            {
                if (dragedCurrentItem.CurrentInventoryItem.id == GetComponent<CurrentItem>().CurrentInventoryItem.id)
                {
                    if (dragedCurrentItem.CurrentInventoryItem.isStackable)
                    {
                        int count = dragedCurrentItem.CurrentInventoryItem.countItem;
                        GetComponent<CurrentItem>().CurrentInventoryItem.countItem += count;
                        dragedCurrentItem.CurrentInventoryItem = InventoryManager.instanceInventory.EmptySlot();
                    }
                }
                else
                {
                    Item currentItem = GetComponent<CurrentItem>().CurrentInventoryItem;
                    GetComponent<CurrentItem>().CurrentInventoryItem = dragedCurrentItem.CurrentInventoryItem;
                    dragedCurrentItem.CurrentInventoryItem = currentItem;
                }

            }

            if (dragedObject.GetComponent<FavoriteItem>())
            {
                if (dragedFavouriteItem.CurrentFavoriteItem.id == GetComponent<CurrentItem>().CurrentInventoryItem.id)
                {
                    if (dragedFavouriteItem.CurrentFavoriteItem.isStackable)
                    {
                        int count = dragedFavouriteItem.CurrentFavoriteItem.countItem;
                        GetComponent<CurrentItem>().CurrentInventoryItem.countItem += count;
                        dragedFavouriteItem.CurrentFavoriteItem = InventoryManager.instanceInventory.EmptySlot();
                    }
                }
                else
                {
                    Item currentItem = GetComponent<CurrentItem>().CurrentInventoryItem;
                    GetComponent<CurrentItem>().CurrentInventoryItem = dragedFavouriteItem.CurrentFavoriteItem;
                    dragedFavouriteItem.CurrentFavoriteItem = currentItem;
                }
            }

            InventoryManager.instanceInventory.DisplayItems();

            if (InventoryManager.instanceInventory.favorite)
                InventoryManager.instanceInventory.favorite.DisplayItems();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GetComponent<CurrentItem>().CurrentInventoryItem.id == InventoryManager.instanceInventory.EmptySlotID())
            {
                if (dragedCurrentItem)
                {
                    Item dragItem = dragedCurrentItem.CurrentInventoryItem.GetCopy();
                    dragItem.countItem = 1;
                    GetComponent<CurrentItem>().CurrentInventoryItem = dragItem;
                    InventoryManager.instanceInventory.RemoveItem(dragedCurrentItem.ItemNum);
                    return;
                }

                if (dragedFavouriteItem)
                {
                    Item dragItem = dragedFavouriteItem.CurrentFavoriteItem.GetCopy();
                    dragItem.countItem = 1;
                    GetComponent<CurrentItem>().CurrentInventoryItem = dragItem;
                    InventoryManager.instanceInventory.favorite.RemoveItem(dragedFavouriteItem.ItemNum);
                    InventoryManager.instanceInventory.DisplayItems();
                    return;
                }
            }

            if (dragedCurrentItem)
            {
                if (GetComponent<CurrentItem>().CurrentInventoryItem.id == dragedCurrentItem.CurrentInventoryItem.id)
                {
                    if (dragedCurrentItem.CurrentInventoryItem.isStackable)
                    {
                        drag.AddItem(GetComponent<CurrentItem>().CurrentInventoryItem);
                        InventoryManager.instanceInventory.RemoveItem(dragedCurrentItem.ItemNum);
                        return;
                    }
                }
            }

            if (dragedFavouriteItem)
            {
                if (GetComponent<CurrentItem>().CurrentInventoryItem.id == dragedFavouriteItem.CurrentFavoriteItem.id)
                {
                    if (dragedFavouriteItem.CurrentFavoriteItem.isStackable)
                    {
                        drag.AddItem(GetComponent<CurrentItem>().CurrentInventoryItem);
                        InventoryManager.instanceInventory.favorite.RemoveItem(dragedFavouriteItem.ItemNum);
                        InventoryManager.instanceInventory.DisplayItems();
                        return;
                    }
                }
            }
        }
    }

}
// списан