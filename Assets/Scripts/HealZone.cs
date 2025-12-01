using UnityEngine;

public class HealZone : MonoBehaviour
{
    public float _healAmount = 5f;
    private PlayerHealth _playerInZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = other.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = null;
        }
    }

    private void Update()
    {
        if (_playerInZone != null)
        {
            _playerInZone.GetHealing(_healAmount * Time.deltaTime);
        }
    }
}