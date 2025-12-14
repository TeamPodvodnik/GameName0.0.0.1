using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    private List<Slot> slots = new List<Slot>();

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;

    [System.Obsolete]
    void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        if (itemDictionary == null) Debug.LogError("ItemDictionary не найден!");

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            slots.Add(slot);
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Инвентарь полон");
        return false;
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
              Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return invData;
    }
    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        // Очищаем содержимое слотов
        foreach (Slot slot in slots)
        {
            if (slot.currentItem != null)
            {
                Destroy(slot.currentItem);
                slot.currentItem = null;
            }
        }

        // Заполняем по данным сохранения
        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < 0 || data.slotIndex >= slots.Count) continue;

            Slot slot = slots[data.slotIndex];
            GameObject itemPrefab = itemDictionary?.GetItemPrefab(data.itemID);
            if (itemPrefab != null)
            {
                GameObject item = Instantiate(itemPrefab, slot.transform, false);
                slot.currentItem = item;
            }
        }
    }

}

/* 
 *  public bool AddItem(GameObject itemPrefab)
    {
        foreach (Slot slot in slots)
        {
            if (slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform, false);
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Инвентарь полон");
        return false;
 * 
 * public List<InventorySaveData> GetInventoryItems()
{
    List<InventorySaveData> invData = new List<InventorySaveData>();
    for (int i = 0; i < slots.Count; i++)
    {
        Slot slot = slots[i];
        Item item = slot.currentItem?.GetComponent<Item>();
        if (item != null)
        {
            invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = i });
        }
    }
    return invData;
}



 999
    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        // Очищаем содержимое слотов
        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
        // Заполняем по данным сохранения
        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)

                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
            GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
            if (itemPrefab != null)
            {
                GameObject item = Instantiate(itemPrefab, slot.transform, false);
                slot.currentItem = item;
            }
        }
    }
} */