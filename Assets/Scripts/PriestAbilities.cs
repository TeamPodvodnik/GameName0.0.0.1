using UnityEngine;
using UnityEngine.InputSystem;

public class PriestAbilities : PlayerAbilities
{
    public GameObject _healSpotPrefab;
    public float _healSpotDuration = 5f;
    public float _healSpotCooldown = 8f;
    public float _healPerSecond = 5f;
    private float _cooldownLeft = 0f;
    private GameObject _activeHealSpot;
    private bool _isUnlocked = false;

    public void UnlockAbility()
    {
        _isUnlocked = true;
    }

    protected override void HandleClassPowers()
    {
        if (!_isUnlocked) return;
        UpdateCooldown();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && _cooldownLeft <= 0f)
        {
            MakeHealSpot();
        }
    }

    private void UpdateCooldown()
    {
        if (_cooldownLeft > 0f) _cooldownLeft -= Time.deltaTime;
    }

    private void MakeHealSpot()
    {
        if (_healSpotPrefab != null)
        {
            _activeHealSpot = Instantiate(_healSpotPrefab, transform.position, Quaternion.identity);
            _cooldownLeft = _healSpotCooldown;

            Destroy(_activeHealSpot, _healSpotDuration);
        }
    }
}