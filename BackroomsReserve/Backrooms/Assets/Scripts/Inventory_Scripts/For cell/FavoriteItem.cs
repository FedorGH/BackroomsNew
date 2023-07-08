using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FavoriteItem : MonoBehaviour, IDropHandler
{
    private int itemNum;

    public Item CurrentFavoriteItem
    {
        get { return InventoryManager.instanceInventory.favorite.favoriteItem[ItemNum]; }
        set { InventoryManager.instanceInventory.favorite.favoriteItem[ItemNum] = value; }
    }

    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }


    void Update()
    {
        if (Input.GetButtonDown((ItemNum + 1).ToString()))
        {
            if (CurrentFavoriteItem.customEvent != null)
            {
                CurrentFavoriteItem.customEvent.Invoke();
            }
            if (CurrentFavoriteItem.forFood)
            {
                InventoryManager.instanceInventory.favorite.RemoveItem(ItemNum);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = Drag.isDraggingObject;

        if (draggedObject == null || draggedObject == gameObject)
        {
            return;
        }

        Drag drag = Drag.isDraggingObject.GetComponent<Drag>();

        FavoriteItem draggedFavouriteItem = draggedObject.GetComponent<FavoriteItem>();
        CurrentItem draggedCurrentItem = draggedObject.GetComponent<CurrentItem>();

        if (Drag.isDraggingObject != gameObject)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (draggedObject.GetComponent<FavoriteItem>())
                {
                    if (draggedFavouriteItem.CurrentFavoriteItem.id == GetComponent<FavoriteItem>().CurrentFavoriteItem.id)
                    {
                        if (draggedFavouriteItem.CurrentFavoriteItem.isStackable)
                        {
                            int count = draggedFavouriteItem.CurrentFavoriteItem.countItem;
                            GetComponent<FavoriteItem>().CurrentFavoriteItem.countItem += count;
                            draggedFavouriteItem.CurrentFavoriteItem = InventoryManager.instanceInventory.EmptySlot();
                        }
                    }
                    else
                    {
                        Item currentItem = GetComponent<FavoriteItem>().CurrentFavoriteItem;
                        GetComponent<FavoriteItem>().CurrentFavoriteItem = draggedFavouriteItem.CurrentFavoriteItem;
                        draggedFavouriteItem.CurrentFavoriteItem = currentItem;
                    }
                }

                if (draggedObject.GetComponent<CurrentItem>())
                {
                    if (draggedCurrentItem.CurrentInventoryItem.id == GetComponent<FavoriteItem>().CurrentFavoriteItem.id)
                    {
                        if (draggedCurrentItem.CurrentInventoryItem.isStackable)
                        {
                            int count = draggedCurrentItem.CurrentInventoryItem.countItem;
                            GetComponent<FavoriteItem>().CurrentFavoriteItem.countItem += count;
                            draggedCurrentItem.CurrentInventoryItem = InventoryManager.instanceInventory.EmptySlot();
                        }
                    }
                    else
                    {
                        Item currentFavouriteItem = GetComponent<FavoriteItem>().CurrentFavoriteItem;
                        GetComponent<FavoriteItem>().CurrentFavoriteItem = draggedCurrentItem.CurrentInventoryItem;
                        draggedCurrentItem.CurrentInventoryItem = currentFavouriteItem;
                    }
                }


                InventoryManager.instanceInventory.DisplayItems();
                InventoryManager.instanceInventory.favorite.DisplayItems();
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (GetComponent<FavoriteItem>().CurrentFavoriteItem.id == InventoryManager.instanceInventory.EmptySlotID())
                {
                    if (draggedCurrentItem)
                    {
                        Item dragItem = draggedCurrentItem.CurrentInventoryItem.GetCopy();
                        dragItem.countItem = 1;
                        GetComponent<FavoriteItem>().CurrentFavoriteItem = dragItem;
                        InventoryManager.instanceInventory.RemoveItem(draggedCurrentItem.ItemNum);
                        InventoryManager.instanceInventory.favorite.DisplayItems();
                        return;
                    }
                    if (draggedFavouriteItem)
                    {
                        Item dragItem = draggedFavouriteItem.CurrentFavoriteItem.GetCopy();
                        dragItem.countItem = 1;
                        GetComponent<FavoriteItem>().CurrentFavoriteItem = dragItem;
                        InventoryManager.instanceInventory.favorite.RemoveItem(draggedFavouriteItem.ItemNum);
                        return;
                    }
                }

                if (draggedCurrentItem)
                {
                    if (GetComponent<FavoriteItem>().CurrentFavoriteItem.id == draggedCurrentItem.CurrentInventoryItem.id)
                    {
                        if (draggedCurrentItem.CurrentInventoryItem.isStackable)
                        {
                            drag.AddItem(GetComponent<FavoriteItem>().CurrentFavoriteItem);
                            InventoryManager.instanceInventory.RemoveItem(draggedCurrentItem.ItemNum);
                            InventoryManager.instanceInventory.favorite.DisplayItems();
                            return;
                        }
                    }
                }

                if (draggedFavouriteItem)
                {
                    if (GetComponent<FavoriteItem>().CurrentFavoriteItem.id == draggedFavouriteItem.CurrentFavoriteItem.id)
                    {
                        if (draggedFavouriteItem.CurrentFavoriteItem.isStackable)
                        {
                            drag.AddItem(GetComponent<FavoriteItem>().CurrentFavoriteItem);
                            InventoryManager.instanceInventory.favorite.RemoveItem(draggedFavouriteItem.ItemNum);
                            return;
                        }
                    }
                }
            }
        }
    }
}
// списан