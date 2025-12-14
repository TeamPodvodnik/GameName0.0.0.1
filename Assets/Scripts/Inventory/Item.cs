using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;


    public int buyPrice = 10; 
    [Range(0, 1)]
    public float sellPrice = 0.6f; // коэффициент тогровцу 


    public int GetSellPrice()
    {
        return Mathf.RoundToInt(buyPrice * sellPrice);
    }

    public virtual void UseItem()
    {
        Debug.Log(  Name + "использован" );
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if (ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }
}
