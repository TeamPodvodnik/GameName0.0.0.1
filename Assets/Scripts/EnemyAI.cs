using UnityEngine;
using UnityEngine.AI;
using GameName.Utils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State _startingState;
    [SerializeField] private float _roamingDistanceMax = 7f;
    [SerializeField] private float _roamingDistanceMin = 3f;
    [SerializeField] private float _roamingTimerMax = 2f;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _chaseRange = 8f;

    private NavMeshAgent _navMeshAgent;
    private State _state;
    private float _roamingTime;
    private float _attackTime;
    private Vector3 _roamPosition;
    private Vector3 _startingPosition;
    private Transform _player;
    private bool _isPlayerIgnored = false;

    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking
    }

    private void Start()
    {
        _startingPosition = transform.position;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _attackTime = 0f;
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _state = _startingState;
    }

    private void Update()
    {
        if (_player == null || _isPlayerIgnored) return;

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
        else if (distanceToPlayer > _chaseRange && _state == State.Chasing)
        {
            _state = State.Roaming;
            Roaming();
        }
        else
        {
            switch (_state)
            {
                default:
                case State.Idle:
                    break;
                case State.Roaming:
                    _roamingTime -= Time.deltaTime;
                    if (_roamingTime < 0)
                    {
                        Roaming();
                        _roamingTime = _roamingTimerMax;
                    }
                    break;
                case State.Chasing:
                    ChasePlayer();
                    break;
                case State.Attacking:
                    _state = State.Chasing;
                    break;
            }
        }
    }

    private void Roaming()
    {
        _roamPosition = GetRoamingPosition();
        _navMeshAgent.SetDestination(_roamPosition);
    }

    private void ChasePlayer()
    {
        _navMeshAgent.SetDestination(_player.position);
    }

    private Vector3 GetRoamingPosition()
    {
        return _startingPosition + Utils.GetRandomDir() * Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }

    private void AttackPlayer()
    {
        _navMeshAgent.SetDestination(transform.position);

        PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(GetComponent<Enemy>().GetDamage());
        }

        _attackTime = _attackCooldown;
    }

    public void SetPlayerIgnored(bool ignored)
    {
        _isPlayerIgnored = ignored;
        if (ignored)
        {
            _state = State.Roaming;
            Roaming();
        }
    }

    public bool IsPlayerIgnored()
    {
        return _isPlayerIgnored;
    }
}