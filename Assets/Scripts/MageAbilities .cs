using UnityEngine;
using UnityEngine.InputSystem;

public class MageAbilities : PlayerAbilities
{
    public GameObject _shieldPrefab;
    public float _shieldCooldown = 10f;
    public int _shieldHealth = 3;

    private GameObject _activeShield;
    private float _cooldownLeft = 0f;
    private int _currentShieldHealth;
    private bool _shieldIsUp = false;
    private bool _isUnlocked = false;

    public void UnlockAbility()
    {
        _isUnlocked = true;
    }

    protected override void HandleClassPowers()
    {
        if (!_isUnlocked) return;

        UpdateCooldown();

        if (Keyboard.current.spaceKey.isPressed && _cooldownLeft <= 0f && !_shieldIsUp)
        {
            SummonShield();
        }

        if (_shieldIsUp)
        {
            MoveShield();
        }
    }

    private void UpdateCooldown()
    {
        if (_cooldownLeft > 0f) _cooldownLeft -= Time.deltaTime;
    }

    private void SummonShield()
    {
        if (_shieldPrefab != null)
        {
            _activeShield = Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
            _activeShield.transform.SetParent(transform);

            MagicShield shieldScript = _activeShield.GetComponent<MagicShield>();
            if (shieldScript != null)
            {
                shieldScript.ConnectToMage(this);
            }

            _shieldIsUp = true;
            _currentShieldHealth = _shieldHealth;
        }
    }

    private void MoveShield()
    {
        if (_activeShield == null) return;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (mouseWorld - (Vector2)transform.position).normalized;
        _activeShield.transform.position = (Vector2)transform.position + lookDir * 1.5f;
        _activeShield.transform.up = lookDir;
    }

    public void ShieldGotHit()
    {
        _currentShieldHealth--;
        if (_currentShieldHealth <= 0)
        {
            RemoveShield();
        }
    }

    private void RemoveShield()
    {
        if (_activeShield != null)
        {
            Destroy(_activeShield);
        }
        _shieldIsUp = false;
        _cooldownLeft = _shieldCooldown;
    }

    public bool IsShieldActive()
    {
        return _shieldIsUp;
    }
}