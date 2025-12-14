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