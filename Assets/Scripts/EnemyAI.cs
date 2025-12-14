using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _chaseRange = 8f;
    [SerializeField] private float _attackDamage = 10f;

    private NavMeshAgent _navMeshAgent;
    private Transform _player;
    private float _roamingTime;
    private float _attackTime;
    private Vector3 _startingPosition;
    private Vector3 _roamPosition;
    private bool _isDead = false;
    private bool _isPlayerIgnored = false;

    private enum State { Roaming, Chasing, Attacking }
    private State _state;

    public void SetAttackDamage(float damage) => _attackDamage = damage;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _startingPosition = transform.position;
        _state = State.Roaming;
        _roamingTime = _roamingTimerMax;
        _attackTime = 0f;
        DifficultySettings.DifficultyLevel diff = DifficultyManager.Instance.GetCurrentDifficulty();
        _attackDamage = diff.enemyDamage;
    }

    private void Update()
    {
        if (_isDead || _isPlayerIgnored) return;
        _attackTime -= Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer <= _attackRange && _attackTime <= 0f)
        {
            _state = State.Attacking;
            AttackPlayer();
        }
        else if (distanceToPlayer <= _detectionRange && distanceToPlayer > _attackRange)
        {
            _state = State.Chasing;
            ChasePlayer();
        }
        else if (distanceToPlayer > _chaseRange && _state == State.Chasing) _state = State.Roaming;

        switch (_state)
        {
            case State.Roaming:
                _roamingTime -= Time.deltaTime;
                if (_roamingTime < 0) { Roaming(); _roamingTime = _roamingTimerMax; }
                break;
            case State.Chasing: ChasePlayer(); break;
        }
    }

    private void Roaming()
    {
        if (_isDead) return;
        _roamPosition = _startingPosition + Random.insideUnitSphere * Random.Range(_roamingDistanceMin, _roamingDistanceMax);
        _roamPosition.z = 0;
        _navMeshAgent.SetDestination(_roamPosition);
    }

    private void ChasePlayer() { if (!_isDead) _navMeshAgent.SetDestination(_player.position); }

    private void AttackPlayer()
    {
        if (_isDead) return;
        _navMeshAgent.SetDestination(transform.position);
        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null) playerHealth.TakeDamage(_attackDamage);
        _attackTime = _attackCooldown;
        _state = State.Chasing;
    }

    public void SetDead()
    {
        _isDead = true;
        _attackTime = 999f;
        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.enabled = false;
            Destroy(_navMeshAgent);
        }
        this.enabled = false;
    }

    public void SetPlayerIgnored(bool ignored)
    {
        _isPlayerIgnored = ignored;
        if (ignored) { _state = State.Roaming; Roaming(); }
    }

    public bool IsPlayerIgnored() => _isPlayerIgnored;
}