using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Potion Item", menuName = "Inventory/Item/NewPotionItem")]
public class PotionItem : ItemScriptObject
{
    private void Start()
    {
        itemType = ItemType.Potion;
    }
}