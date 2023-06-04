using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {Default, Food, Weapon, Instrument};
public class InventoryItem : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType type;
    public string itemName;
    public Sprite icon;
    public int maxAmount;
    public string itemDescription;
}