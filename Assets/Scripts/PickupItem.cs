using Unity.VisualScripting;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("Настройки")]
    public string pickupTag = "Item";
    public KeyCode pickupKey = KeyCode.F;
    public float pickupRange = 2f;

    private GameObject _nearestItem; // первый предмет к игроку
    void Update()
    {
        FindNearestPickup();
        if (_nearestItem != null && Input.GetKeyDown(pickupKey))
        {
            PickupItem(_nearestItem);
        }
    }
    void FindNearestPickup()
    {
        _nearestItem = null;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(pickupTag) && hit.gameObject != gameObject)
            {
                _nearestItem = hit.gameObject;
                return;
            }
        }
    }
}
