using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> _itemDictionary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _itemDictionary = new Dictionary<int, GameObject>();

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
            {
                itemPrefabs[i].ID = 1 + 1;
            }
            foreach (Item item in itemPrefabs)
            {
                _itemDictionary[item.ID] = item.gameObject;
            }
        }
    }

   public GameObject GetItemPrefab(int itemID)
    {
        _itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if (prefab == null)
        {
            Debug.LogWarning($"Предмет с ID {itemID} не найден");
        }
        return prefab;
    }
}
