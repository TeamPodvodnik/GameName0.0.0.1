using UnityEngine;
using UnityEngine.InputSystem;

public class SpearWeapon : ClassWeapon
{
    public float attackDistance = 1.5f;
    public float attackSpeed = 10f;
    public float returnSpeed = 8f;

    private Animator _spearAnimator;
    private Collider2D _spearCollider;
    private Transform _playerTransform;
    private Vector3 _originalLocalPos;
    private bool _isAttacking = false;
    private float _attackProgress = 0f;

    void Start()
    {
        _spearAnimator = GetComponent<Animator>();
        _spearCollider = GetComponent<Collider2D>();
        _playerTransform = _player.transform;
        _originalLocalPos = transform.localPosition;

        if (_spearCollider != null)
        {
            _spearCollider.enabled = false;
            _spearCollider.isTrigger = true;
        }
        IgnorePlayerCollision();
    }

    void IgnorePlayerCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && _spearCollider != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(_spearCollider, playerCollider, true);
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!_isAttacking && UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame && _canAttack)
        {
            StartAttack();
        }

        UpdateSpearDirection();
        UpdateAttackMovement();
    }

    void UpdateSpearDirection()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 playerPos = _playerTransform.position;
        Vector2 direction = ((Vector2)mouseWorld - playerPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + -90f;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void UpdateAttackMovement()
    {
        if (_isAttacking)
        {
            _attackProgress += Time.deltaTime * attackSpeed;
            if (_attackProgress >= 1f)
            {
                _attackProgress = 1f;
                _isAttacking = false;
            }
        }
        else
        {
            _attackProgress -= Time.deltaTime * returnSpeed;
            if (_attackProgress <= 0f)
            {
                _attackProgress = 0f;
            }
        }

        float currentDistance = Mathf.Lerp(0f, attackDistance, _attackProgress);
        transform.localPosition = _originalLocalPos + transform.up * currentDistance;
    }

    void StartAttack()
    {
        PerformAttack();
        _isAttacking = true;

        if (_spearAnimator != null)
        {
            _spearAnimator.SetTrigger("Attack");
        }

        StartCoroutine(EnableColliderForAttack());
    }

    System.Collections.IEnumerator EnableColliderForAttack()
    {
        EnableSpearCollider();
        yield return new WaitForSeconds(0.3f);
        DisableSpearCollider();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_spearCollider == null || !_spearCollider.enabled) return;

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

    public void EnableSpearCollider()
    {
        if (_spearCollider != null)
        {
            _spearCollider.enabled = true;
        }
    }

    public void DisableSpearCollider()
    {
        if (_spearCollider != null)
        {
            _spearCollider.enabled = false;
        }
    }
}