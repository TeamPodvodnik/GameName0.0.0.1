using UnityEngine;
using UnityEngine.InputSystem;

public class StaffWeapon : ClassWeapon
{
    public GameObject fireballPrefab;
    public float projectileSpeed = 10f;

    private Transform _firePoint;

    void Start()
    {
        _firePoint = transform.Find("FirePoint");
        if (_firePoint == null)
        {
            _firePoint = transform;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (Mouse.current.leftButton.wasPressedThisFrame && _canAttack)
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        PerformAttack();

        if (fireballPrefab != null && _classData != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = (mouseWorld - (Vector2)_firePoint.position).normalized;

            GameObject fireball = Instantiate(fireballPrefab, _firePoint.position, Quaternion.identity);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;
            }

            fireball.transform.up = direction;

            Fireball fireballScript = fireball.GetComponent<Fireball>();
            if (fireballScript != null)
            {
                fireballScript.damage = _classData.weaponDamage;
            }
        }
    }
}