using UnityEngine;

public class ClassWeapon : MonoBehaviour
{
    protected CharacterClassData _classData;
    protected GameObject _player;
    protected bool _canAttack = true;
    protected float _cooldownTimer = 0f;

    public void SetupWeapon(CharacterClassData classData, GameObject player)
    {
        _classData = classData;
        _player = player;
    }

    protected virtual void Update()
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

    protected virtual void PerformAttack()
    {
        if (!_canAttack) return;
        _canAttack = false;

        if (_classData != null)
        {
            _cooldownTimer = _classData.attackCooldown;
        }
    }
}