using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float _moveSpeed = 8f;
    private Rigidbody2D _body;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 moveDir = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveDir.y += 1;
        if (Keyboard.current.sKey.isPressed) moveDir.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveDir.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveDir.x += 1;

        _body.linearVelocity = moveDir * _moveSpeed;

        if (moveDir.x < 0) _sprite.flipX = true;
        else if (moveDir.x > 0) _sprite.flipX = false;
    }
}