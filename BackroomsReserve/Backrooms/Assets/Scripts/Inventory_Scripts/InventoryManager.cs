using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    List<InventoryItem> items;
    private void Start()
    {
        items = new List<InventoryItem>();
    }
}
