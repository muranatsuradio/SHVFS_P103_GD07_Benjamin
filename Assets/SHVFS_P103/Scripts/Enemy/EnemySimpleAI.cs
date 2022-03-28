using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Stunned,
    Attracted,
    Dead,
}

public class EnemySimpleAI : MonoBehaviour
{
    public EnemyState EnemyState;

    public Transform StartPatrolPosition;
    public Transform EndPatrolPosition;
    public float MinDistance = 1.5f;

    public float AggroRadius = 5.0f;
    public float MeleeRadius = 1.5f;

    public bool IsAttacking = false;

    private float _attackCoolDown;
    private const float _ATTACK_COOL_DOWN = 1.2f;

    private Vector3 _currentDestination;
    private NavMeshAgent _navMeshAgent;

    private GameObject _playerGameObject;

    private EnemyStatusComponent _enemyStatus;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _currentDestination = StartPatrolPosition.position;

        _playerGameObject = GameObject.FindGameObjectWithTag("Player");

        _enemyStatus = GetComponent<EnemyStatusComponent>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, AggroRadius);
    }

    private void Update()
    {
        switch (EnemyState)
        {
            case EnemyState.Idle:
                SearchForPlayer();
                break;
            case EnemyState.Patrol:
                SearchForPlayer();
                Patrol();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
            case EnemyState.Stunned:
                break;
            case EnemyState.Attracted:
                break;
            case EnemyState.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #region EnemyAction

    private void Patrol()
    {
        if (!StartPatrolPosition || !EndPatrolPosition) return;

        if (_navMeshAgent && _currentDestination != Vector3.zero)
        {
            _navMeshAgent.SetDestination(_currentDestination);
        }

        var distance = Vector3.Distance(_currentDestination, transform.position);

        if (distance >= MinDistance) return;

        _currentDestination = _currentDestination == EndPatrolPosition.position
            ? StartPatrolPosition.position
            : EndPatrolPosition.position;
    }

    private void ChasePlayer()
    {
        if (!_navMeshAgent || _currentDestination == Vector3.zero) return;

        _currentDestination = _playerGameObject.transform.position;
        _navMeshAgent.SetDestination(_currentDestination);

        var distance = Vector3.Distance(transform.position, _currentDestination);

        if (distance >= MinDistance)
        {
            _navMeshAgent.isStopped = false;
            return;
        }

        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;
        EnemyState = EnemyState.Attack;
    }

    private void SearchForPlayer()
    {
        var distance = Vector3.Distance(transform.position, _playerGameObject.transform.position);

        if (distance >= AggroRadius) return;

        EnemyState = EnemyState.Chase;
    }

    private void UpdateAttack()
    {
        var distance = Vector3.Distance(transform.position, _playerGameObject.transform.position);
        if (distance >= MeleeRadius)
        {
            EnemyState = EnemyState.Chase;
            IsAttacking = false;
            return;
        }

        _attackCoolDown -= Time.deltaTime;

        if (!(_attackCoolDown <= 0f)) return;

        var playerStatus = _playerGameObject.GetComponent<PlayerStatusComponent>();

        if (_enemyStatus && playerStatus)
        {
            playerStatus.TakeDamage(_enemyStatus.Damage);
        }

        IsAttacking = true;
        
        _attackCoolDown = _ATTACK_COOL_DOWN;
    }

    #endregion
}