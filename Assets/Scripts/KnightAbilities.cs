using UnityEngine;
using UnityEngine.InputSystem;

public class KnightAbilities : PlayerAbilities
{
    public float _dashPower = 20f;
    public float _dashTime = 0.2f;
    public float _dashWait = 1f;

    private Rigidbody2D _body;
    private bool _isDashing = false;
    private float _dashCounter = 0f;
    private float _waitCounter = 0f;
    private Vector2 _dashDir;

    public void SetupClassAbilities()
    {
        if (_currentClass == null || _currentClass.displayName.Contains("׀ûצאנü"))
        {
            enabled = true;
            return;
        }

        _body = GetComponent<Rigidbody2D>();
    }

    private void ContinueDash()
    {
        if (_body == null) return;

        _dashCounter -= Time.deltaTime;
        _body.linearVelocity = _dashDir * _dashPower;

        if (_dashCounter <= 0f)
        {
            _isDashing = false;
        }
    }

    protected override void HandleClassPowers()
    {
        UpdateTimers();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && _waitCounter <= 0f)
        {
            StartDashing();
        }

        if (_isDashing)
        {
            ContinueDash();
        }
    }

    private void UpdateTimers()
    {
        if (_waitCounter > 0f) _waitCounter -= Time.deltaTime;
    }

    private void StartDashing()
    {
        Vector2 moveDir = GetMovementDirection();

        if (moveDir != Vector2.zero)
        {
            _isDashing = true;
            _dashCounter = _dashTime;
            _waitCounter = _dashWait;
            _dashDir = moveDir.normalized;
        }
    }

    private Vector2 GetMovementDirection()
    {
        Vector2 dir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) dir.y += 1;
        if (Keyboard.current.sKey.isPressed) dir.y -= 1;
        if (Keyboard.current.aKey.isPressed) dir.x -= 1;
        if (Keyboard.current.dKey.isPressed) dir.x += 1;

        return dir;
    }
}