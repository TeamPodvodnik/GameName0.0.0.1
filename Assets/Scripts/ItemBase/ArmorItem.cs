using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Armor Item", menuName = "Inventory/Item/NewArmorItem")]
public class ArmorItem : ItemScriptObject

{
    private void Start()
    {
        itemType = ItemType.Armor;
    }
}
