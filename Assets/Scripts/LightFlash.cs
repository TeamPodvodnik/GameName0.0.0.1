using UnityEngine;

public class LightFlash : MonoBehaviour
{
    public float damage = 25f;
    public float duration = 0.3f;

    private Vector2 _direction;
    private float _range;
    private float _width;
    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        DrawFlash();
        Destroy(gameObject, duration);
    }

    public void SetupFlash(Vector2 direction, float range, float width)
    {
        _direction = direction;
        _range = range;
        _width = width;
    }

    private void DrawFlash()
    {
        if (_lineRenderer != null)
        {
            Vector2 startPoint = transform.position;
            Vector2 endPoint = startPoint + _direction * _range;

            _lineRenderer.SetPosition(0, startPoint);
            _lineRenderer.SetPosition(1, endPoint);
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _width;

            CheckHit(startPoint, endPoint);
        }
    }

    private void CheckHit(Vector2 start, Vector2 end)
    {
        RaycastHit2D[] hits = Physics2D.CapsuleCastAll(start, new Vector2(_width, _range),
            CapsuleDirection2D.Vertical, 0f, _direction, _range);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}