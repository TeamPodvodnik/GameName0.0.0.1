using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Weapon Item", menuName = "Inventory/Item/NewWeaponItem")]
public class WeaponItem : ItemScriptObject
{
    private void Start()
    {
        itemType = ItemType.Weapon;
    }
}
