using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
        if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
        if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
        if (Keyboard.current.dKey.isPressed) moveInput.x += 1;

        rb.linearVelocity = moveInput * moveSpeed;

        if (moveInput.x < 0) spriteRenderer.flipX = true;
        else if (moveInput.x > 0) spriteRenderer.flipX = false;
    }
}