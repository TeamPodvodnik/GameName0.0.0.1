using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private bool isTakingDamage = false;
    private EnemyAI enemyAI;
    private NavMeshAgent navMeshAgent;

    public Room room;

    void Start()
    {
        DifficultySettings.DifficultyLevel currentDifficulty = DifficultyManager.Instance.GetCurrentDifficulty();
        currentHealth = (int)currentDifficulty.enemyHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (enemyAI != null) enemyAI.SetAttackDamage(currentDifficulty.enemyDamage);
    }

    public void TakeDamage(float damage)
    {
        if (isTakingDamage) return;
        currentHealth -= (int)damage;
        StartCoroutine(DamageEffect());
        if (currentHealth <= 0) DieImmediately();
    }

    IEnumerator DamageEffect()
    {
        isTakingDamage = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
        isTakingDamage = false;
    }

    void DieImmediately()
    {
        if (enemyAI != null) Destroy(enemyAI);
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
            Destroy(navMeshAgent);
        }

        Collider2D col = GetComponentInChildren<Collider2D>();
        if (col != null) col.enabled = false;

        PlayerXP playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXP>();
        if (playerXP != null)
        {
            playerXP.AddXP(10f);
        }

        room.EnemyDied();


        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        Transform visual = spriteRenderer.transform;
        visual.rotation = Quaternion.Euler(0, 0, 90f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}