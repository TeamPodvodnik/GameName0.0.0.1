using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController _inventoryController;

    [System.Obsolete]
    void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                bool itemAdded = _inventoryController.AddItem(collision.gameObject);

                if (itemAdded)
                { 
                    item.PickUp();
                    Destroy(collision.gameObject); 
                }
            }
        }
    }
}
