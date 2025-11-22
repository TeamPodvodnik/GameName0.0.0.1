using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class InventoryManager : MonoBehaviour
{
    public GameObject UIPanel;
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    void Start()
    {

        for (int i = 0; i < inventoryPanel.childCount; i++) // обращение ко всем дочкам по порядку
        {
            if (inventoryPanel.GetChild(0).GetComponent<InventorySlot>() != null) //проверка на работу дочек
            {
                slots.Add(inventoryPanel.GetChild(0).GetComponent<InventorySlot>()); // восстановление дочек
            }
        }
        UIPanel.SetActive(false); // выключенный инвентарь вначале 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIPanel.SetActive(true);
            }
            else
            {
                UIPanel.SetActive(false);
            }
        }
    } 
}

  

