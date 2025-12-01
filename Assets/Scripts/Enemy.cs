using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    private EnemyAI _enemyAI;

    private void Start()
    {
        _enemyAI = GetComponentInParent<EnemyAI>();
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
        if (_enemyAI != null)
        {
            _enemyAI.enabled = false;
        }

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MageAbilities mage = collision.gameObject.GetComponent<MageAbilities>();
            if (mage == null || !mage.IsShieldActive())
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(GetDamage());
                }
            }
        }
    }

    public float GetDamage()
    {
        return _damage;
    }
}