using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveController : MonoBehaviour
{
    private string savelocation;
    private InventoryController _inventoryController;
    private HotbarController _hotbarController;

    [System.Obsolete]
    void Start()
    {
        savelocation = Path.Combine(Application.persistentDataPath, "SaveData.json");
        _inventoryController = FindObjectOfType<InventoryController>();
        _hotbarController = FindObjectOfType<HotbarController>();

        LoadGame();
    }

    // Update is called once per frame
    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            inventorySaveData = _inventoryController.GetInventoryItems(),
            hotbarSaveData = _hotbarController.GetHotbarItems()
        };
        File.WriteAllText(savelocation, JsonUtility.ToJson(saveData));

    }
    public void LoadGame()
    {
        if (File.Exists(savelocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(savelocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            _inventoryController.SetInventoryItems(saveData.inventorySaveData);
            _hotbarController.SetHotbarItems(saveData.hotbarSaveData);
        }
        else
        {
            SaveGame();
        }
    }
}
