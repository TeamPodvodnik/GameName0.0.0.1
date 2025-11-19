using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName ="Food Item",menuName ="Inventory/Item/NewFoodItem")]
public class FoodItem : ItemScriptObject
{
    public float healAmount;

private void Start()
    {
        itemType = ItemType.Food;
    }

}
