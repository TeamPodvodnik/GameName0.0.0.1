using UnityEngine;
using UnityEngine.InputSystem;

public class SwordWeapon : ClassWeapon
{
    private Animator _weaponAnimator;
    private Collider2D _swordCollider;
    private Transform _playerTransform;
    private bool _facingRight = true;
    private Transform _weaponPivot;

    void Start()
    {
        _weaponAnimator = GetComponent<Animator>();
        _swordCollider = GetComponent<Collider2D>();
        _playerTransform = _player.transform;

        _weaponPivot = transform.parent;

        if (_swordCollider != null)
        {
            _swordCollider.enabled = false;
            _swordCollider.isTrigger = true;
        }

        IgnorePlayerCollision();
    }

    void IgnorePlayerCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && _swordCollider != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(_swordCollider, playerCollider, true);
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame && _canAttack)
        {
            StartAttack();
        }

        UpdateWeaponDirection();
    }

    void TestTurnWeapon()
    {
        _facingRight = !_facingRight;

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.z = _facingRight ? 0f : 180f;
        transform.localEulerAngles = newRotation;
    }

    private void UpdateWeaponDirection()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 playerPosition = _playerTransform.position;

        if (_weaponPivot != null)
        {
            Vector3 newScale = _weaponPivot.localScale;

            if (mouseWorld.x < playerPosition.x)
            {
                newScale.x = -Mathf.Abs(newScale.x);
            }
            else
            {
                newScale.x = Mathf.Abs(newScale.x);
            }

            _weaponPivot.localScale = newScale;
        }
    }

    void StartAttack()
    {
        PerformAttack();

        if (_weaponAnimator != null)
        {
            _weaponAnimator.SetTrigger("Attack");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_swordCollider == null || !_swordCollider.enabled) return;

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null && _classData != null)
            {
                float finalDamage = _classData.weaponDamage * PlayerBonus.DamageMultiplier;
                enemy.TakeDamage(finalDamage);
            }
        }
    }

    public void EnableSwordCollider()
    {
        if (_swordCollider != null)
        {
            _swordCollider.enabled = true;
        }
    }

    public void DisableSwordCollider()
    {
        if (_swordCollider != null)
        {
            _swordCollider.enabled = false;
        }
    }
}