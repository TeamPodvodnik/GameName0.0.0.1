using UnityEngine;
using UnityEngine.InputSystem;

public class PriestStaff : ClassWeapon
{
    public GameObject lightFlashPrefab;
    public float flashWidth = 2f;
    public float flashRange = 8f;

    private Transform _firePoint;

    void Start()
    {
        _firePoint = transform.Find("FirePoint");
        if (_firePoint == null)
        {
            _firePoint = transform;
        }
    }

    void Update()
    {
        base.Update();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            CastLightFlash();
        }
    }

    private void CastLightFlash()
    {
        PerformAttack();

        if (lightFlashPrefab != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = (mouseWorld - (Vector2)_firePoint.position).normalized;

            GameObject flash = Instantiate(lightFlashPrefab, _firePoint.position, Quaternion.identity);
            flash.transform.up = direction;

            LightFlash flashScript = flash.GetComponent<LightFlash>();
            if (flashScript != null)
            {
                flashScript.SetupFlash(direction, flashRange, flashWidth);
            }
        }
    }
}