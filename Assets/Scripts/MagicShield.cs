using UnityEngine;

public class MagicShield : MonoBehaviour
{
    private MageAbilities _mage;
    private Collider2D _shieldCollider;

    void Start()
    {
        _shieldCollider = GetComponent<Collider2D>();
        if (_shieldCollider != null)
        {
            _shieldCollider.isTrigger = false;
        }
    }

    public void ConnectToMage(MageAbilities mage)
    {
        _mage = mage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _mage.ShieldGotHit();
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                enemyRb.AddForce(pushDirection * 5f, ForceMode2D.Impulse);
            }
        }
    }
}