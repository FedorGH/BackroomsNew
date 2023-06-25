using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public int id;
    public int countItem;
    public bool isStackable;
    [Multiline(5)]
    public string itemDescription;
    public string iconItem;
    public string prefabItem;

}