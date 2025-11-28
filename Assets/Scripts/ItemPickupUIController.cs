using UnityEngine;

public class ItemPickupUIController : MonoBehaviour
{
    public static ItemPickupUIController Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopups = 5;
    public float popupDuration;

  //  private readonly QueueMode<GameObject> _activePopups = new();

 /*   private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError (123);
        }
    } */
}
