using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _baseXP = 10f;
    [SerializeField] private float _xpMultiplier = 1f;

    private int _currentHealth;
    private SpriteRenderer _spriteRenderer;
    private bool _isTakingDamage = false;
    private EnemyAI _enemyAI;
    private NavMeshAgent _navMeshAgent;

    public Room room;

    void Start()
    {
        DifficultySettings.DifficultyLevel currentDifficulty = DifficultyManager.Instance.GetCurrentDifficulty();
        _currentHealth = (int)currentDifficulty.enemyHealth;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _enemyAI = GetComponent<EnemyAI>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_enemyAI != null) _enemyAI.SetAttackDamage(currentDifficulty.enemyDamage);
    }

    public void TakeDamage(float damage)
    {
        if (_isTakingDamage) return;
        _currentHealth -= (int)damage;
        StartCoroutine(DamageEffect());
        if (_currentHealth <= 0) DieImmediately();
    }

    IEnumerator DamageEffect()
    {
        _isTakingDamage = true;
        Color originalColor = _spriteRenderer.color;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = originalColor;
        _isTakingDamage = false;
    }

    void DieImmediately()
    {
        if (_enemyAI != null) Destroy(_enemyAI);
        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = true;
            Destroy(_navMeshAgent);
        }

        Collider2D col = GetComponentInChildren<Collider2D>();
        if (col != null) col.enabled = false;

        PlayerXP playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXP>();
        if (playerXP != null)
        {
            float finalXP = _baseXP * _xpMultiplier;
            playerXP.AddXP(finalXP);
        }

        if (room != null) room.EnemyDied();

        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        Transform visual = _spriteRenderer.transform;
        visual.rotation = Quaternion.Euler(0, 0, 90f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}