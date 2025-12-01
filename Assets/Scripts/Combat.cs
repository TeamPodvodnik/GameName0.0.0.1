using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject _weapon;
    private Animator _weaponAnimator;

    private void Start()
    {
        if (_weapon == null)
        {
            _weapon = GameObject.Find("Sword");
        }

        if (_weapon != null)
        {
            _weaponAnimator = _weapon.GetComponent<Animator>();
        }
        else
        {
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (_weaponAnimator != null)
        {
            _weaponAnimator.SetTrigger("Attack");
        }
    }
}