using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopNPC : MonoBehaviour
{
    public string shopID = "shop_merchant_01";
    public string shopkeeperName = "Merchant";

    public List<ShopStockItem> defaultShopStock = new();
    private List<ShopStockItem> _currentShopStock = new();

    private bool isInitialized = false;

    [System.Serializable]
    public class ShopStockItem
    {
        public int itemID;
        public int quantity;
    }

    void Start()
    {
        InitializeShop();
    }

  private void InitializeShop()
    {
        if(isInitialized) return;

        _currentShopStock = new List<ShopStockItem>();
            foreach (var item in defaultShopStock)
        {
            _currentShopStock.Add(new ShopStockItem()
            {
                itemID = item.itemID,
                quantity = item.quantity,
            });
            }
            isInitialized = true;
    }

    public List<ShopStockItem> GetCurrentStock()
    {
        return _currentShopStock;
    }

    public void SetStock(List<ShopStockItem> stock)
    {
        _currentShopStock = stock;
    }

    public void AddToStock(int itemID, int quantity)
    {
        ShopStockItem existing = _currentShopStock.Find(s => s.itemID == itemID);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            _currentShopStock.Add(new ShopStockItem { itemID = itemID, quantity = quantity });
        }
    }


    public bool RemoveFromShopStock(int itemID, int quantity)
    {
        ShopStockItem existing = _currentShopStock.Find(s => s.itemID == itemID);
        if (existing != null && existing.quantity >= quantity)
        {
            existing.quantity -= quantity;
            return true;
        }
        return false;
    }
}

