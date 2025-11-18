using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _invincibilityTime = 1f;

    private float _currentHealth;
    private float _invincibilityTimer;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _invincibilityTimer = 0f;
    }

    private void Update()
    {
        if (_invincibilityTimer > 0)
        {
            _invincibilityTimer -= Time.deltaTime;

            float flashSpeed = 10f;
            float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f);
            _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            if (_invincibilityTimer <= 0)
            {
                _spriteRenderer.color = _originalColor;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_invincibilityTimer <= 0)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    TakeDamage(enemy.GetDamage());
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (_invincibilityTimer > 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        _invincibilityTimer = _invincibilityTime;
        _spriteRenderer.color = Color.white;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }
}