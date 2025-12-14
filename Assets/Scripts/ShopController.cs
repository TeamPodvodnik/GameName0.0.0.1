using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
   public static ShopController instance;

    [Header("UI")]
    public GameObject shopPanel;
    public Transform shopInventoryGrid, playerInventoryGrid;
    public GameObject shopSlotPrefab;
    public TMP_Text playerMoneyText, ShopTitleText;

    private ItemDictionary itemDictionary;
    private ShopNPC currentShop;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();
        shopPanel.SetActive(false);

        if (CurrencyController.Instance != null)
        {
            CurrencyController.Instance.OnGoldChanged += UpdateMoneyDisplay;
            UpdateMoneyDisplay(CurrencyController.Instance.GetGold());
        }
    }
    private void UpdateMoneyDisplay(int amount)
    {
        if (playerMoneyText != null) 
            playerMoneyText.text = amount.ToString();
    }
    public void OpenShop(ShopNPC shop)
    {
        currentShop = shop;
        shopPanel.SetActive(true);
        if (ShopTitleText != null) ShopTitleText.text = shop.shopkeeperName + "'s Магазин "; // имя торговца плюс магазин
        //RefreshShopDisplay
        //RefreshPlayerInventoryDisplay
        
    }
    public void CloseShop()
    {
        shopPanel.SetActive(false );
        currentShop = null;
    }
}
