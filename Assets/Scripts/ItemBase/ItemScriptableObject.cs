using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType { Default, Food, Weapon, Potion, Armor}
public class ItemScriptObject : ScriptableObject
{
    public ItemType itemType;
    public string itemName; // имя
    public int maxAmount; //количество
    public string itemDescription; // описание
}
