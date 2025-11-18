using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    private void Start()
    {
        InitializeFromDifficulty();
    }

    private void InitializeFromDifficulty()
    {
        if (DifficultyManager.Instance != null)
        {
            var currentDifficulty = DifficultyManager.Instance.GetCurrentDifficulty();
            _health = currentDifficulty.enemyHealth;
            _damage = currentDifficulty.enemyDamage;
        }
        else
        {
            _health = 100f;
            _damage = 20f;
        }
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return _damage;
    }
}