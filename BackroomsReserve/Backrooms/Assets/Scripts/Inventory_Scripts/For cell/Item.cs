using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public string itemName;
    public int id;
    public int countItem;
    public bool isStackable;
    [Multiline(5)]
    public string itemDescription;
    public Sprite iconItem;

    public bool forFood;
    public bool isDropped;

    public UnityEvent customEvent;
    public Item GetCopy()
    {
        return (Item)this.MemberwiseClone();
    }
}

// списан