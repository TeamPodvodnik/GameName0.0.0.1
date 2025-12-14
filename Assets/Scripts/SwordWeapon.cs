using UnityEngine;
using UnityEngine.InputSystem;

public class SwordWeapon : ClassWeapon
{
    private Animator _weaponAnimator;

    void Start()
    {
        _weaponAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        base.Update();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SwingSword();
        }
    }

    private void SwingSword()
    {
        PerformAttack();

        if (_weaponAnimator != null)
        {
            _weaponAnimator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canAttack) return;

        Debug.Log("Меч столкнулся с: " + other.gameObject.name);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Враг обнаружен!");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Наносим урон: " + _classData.weaponDamage);
                enemy.TakeDamage(_classData.weaponDamage);
            }
            else
            {
                Debug.LogError("Enemy компонент не найден!");
            }
        }
    }
}