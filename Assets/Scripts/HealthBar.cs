using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private PlayerHealth playerHealth;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindAnyObjectByType<PlayerHealth>();
        }
        
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not found!");
        }
    }

    private void Update()
    {
        if (playerHealth != null && healthFill != null)
        {
            float healthPercent = playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
            healthFill.fillAmount = healthPercent;
        }
    }
}