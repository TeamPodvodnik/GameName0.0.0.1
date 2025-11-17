using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float damage;

    private void Start()
    {
        if (DifficultyManager.Instance != null)
        {
            var currentDifficulty = DifficultyManager.Instance.GetCurrentDifficulty();
            health = currentDifficulty.enemyHealth;
            damage = currentDifficulty.enemyDamage;
            Debug.Log($"Enemy: {health} HP, {damage} damage ({currentDifficulty.name})");
        }
        else
        {
            Debug.LogError("DifficultyManager is NULL!");
            damage = 20f;
        }
        InitializeFromDifficulty();
    }

    private void InitializeFromDifficulty()
    {
        if (DifficultyManager.Instance != null)
        {
            var currentDifficulty = DifficultyManager.Instance.GetCurrentDifficulty();
            health = currentDifficulty.enemyHealth;
            damage = currentDifficulty.enemyDamage;

            Debug.Log($"Enemy initialized with: {health} HP and {damage} damage " +
                     $"({currentDifficulty.name} difficulty)");
        }
        else
        {
            health = 100f;
            damage = 20f;
            Debug.LogWarning("DifficultyManager not found! Using default values.");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log($"Enemy took {amount} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log($"Enemy collided with player! Dealing {damage} damage");
                playerHealth.TakeDamage(damage);
            }
        }
    }
}