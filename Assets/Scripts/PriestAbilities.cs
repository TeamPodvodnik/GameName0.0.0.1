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

    protected override void HandleClassPowers()
    {
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