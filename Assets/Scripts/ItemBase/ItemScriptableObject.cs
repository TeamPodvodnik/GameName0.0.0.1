using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType { Default, Food, Weapon, Potion, Armor} // типы предметов потом мб добавлю еще
public class ItemScriptObject : ScriptableObject
{
   
    public string itemName; // имя
    public ItemType itemType; // Поиск и настройка по типу (по иее мне поможет)
    public GameObject itemPrefab; // буквально сама вещь (скрипту через скрипт даешь право быть вещью, поганая алхимия)
    public int maxAmount; //количество
    public string itemDescription; // описание

}
