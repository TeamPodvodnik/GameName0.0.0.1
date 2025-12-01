using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _attackDamage = 25f;
    [SerializeField] private float _attackCooldown = 0.5f;

    private bool _canAttack = true;
    private float _cooldownTimer = 0f;

    private void Update()
    {
        if (!_canAttack)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0f)
            {
                _canAttack = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canAttack) return;

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_attackDamage);
                _canAttack = false;
                _cooldownTimer = _attackCooldown;
            }
        }
    }

    public void SetDamage(float damage)
    {
        _attackDamage = damage;
    }
}