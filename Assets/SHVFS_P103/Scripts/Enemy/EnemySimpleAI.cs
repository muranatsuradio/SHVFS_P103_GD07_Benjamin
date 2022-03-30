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
    Attracted,
}

public class EnemySimpleAI : MonoBehaviour
{
    public EnemyState EnemyState;

    public Transform StartPatrolPosition;
    public Transform EndPatrolPosition;
    public float MinDistance = 1.5f;

    public float CanHearRadius = 10.0f;
    public float AggroRadius = 5.0f;
    public float MeleeRadius = 1.5f;

    public bool IsAttacking = false;

    public EnemyAnimationComponent EnemyAnimationComponent;

    private float _attackCoolDown;
    private const float _ATTACK_COOL_DOWN = 1.2f;

    public bool IsStunned = false;
    private const float _STUNNED_TIME = 5f;

    public Vector3 CurrentDestination;
    private NavMeshAgent _navMeshAgent;

    private GameObject _playerGameObject;

    private EnemyStatusComponent _enemyStatus;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        CurrentDestination = StartPatrolPosition.position;

        _playerGameObject = GameObject.FindGameObjectWithTag("Player");

        _enemyStatus = GetComponent<EnemyStatusComponent>();

        if (this)
        {
            PistolShootComponent.OnPlayerShoot += HearPlayerShoot;
        }
    }

    private void Update()
    {
        if (!_playerGameObject || !_navMeshAgent || !_enemyStatus) return;

        if (_enemyStatus.IsDead)
        {
            _navMeshAgent.isStopped = true;
            EnemyState = EnemyState.Idle;
        }
        
        if (_enemyStatus.IsDead || IsStunned) return;

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
            case EnemyState.Attracted:
                SearchForPlayer();
                Attracted();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    #region EnemyAction

    private void Patrol()
    {
        if (!StartPatrolPosition || !EndPatrolPosition) return;
        
        _navMeshAgent.SetDestination(CurrentDestination);

        var distance = Vector3.Distance(CurrentDestination, transform.position);

        if (distance >= MinDistance) return;

        CurrentDestination = CurrentDestination == EndPatrolPosition.position
            ? StartPatrolPosition.position
            : EndPatrolPosition.position;
    }

    private void ChasePlayer()
    {
        if (!_navMeshAgent || CurrentDestination == Vector3.zero) return;

        CurrentDestination = _playerGameObject.transform.position;
        _navMeshAgent.SetDestination(CurrentDestination);

        var distance = Vector3.Distance(transform.position, CurrentDestination);

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
        var direction = _playerGameObject.transform.position - transform.position;

        if (!(Vector3.Dot(direction, transform.forward) >= 0)) return;
        
        var distance = direction.magnitude;

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

        IsAttacking = true;

        _attackCoolDown = _ATTACK_COOL_DOWN;
    }

    private void Attracted()
    {
        if (CurrentDestination == Vector3.zero) return;
        
        if (EnemyState == EnemyState.Chase || EnemyState == EnemyState.Attack) return;

        _navMeshAgent.SetDestination(CurrentDestination);

        var distance = Vector3.Distance(transform.position, CurrentDestination);

        if (distance >= MinDistance)
        {
            _navMeshAgent.isStopped = false;
            return;
        }

        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;

        IsStunned = true;
        StartCoroutine(BeStunned(EnemyState.Patrol));
    }

    private IEnumerator BeStunned(EnemyState lastState)
    {
        yield return new WaitForSeconds(_STUNNED_TIME);

        EnemyState = lastState;
        _navMeshAgent.isStopped = false;
        IsStunned = false;
    }

    private void HearPlayerShoot()
    {
        if (!this) return;
        
        var distance = Vector3.Distance(transform.position, _playerGameObject.transform.position);
        if (!(distance <= CanHearRadius)) return;
        
        CurrentDestination =_playerGameObject.transform.position; 
            
        EnemyState = EnemyState.Attracted;
    }
    #endregion

    public void ApplyDamage()
    {
        var playerStatus = _playerGameObject.GetComponent<PlayerStatusComponent>();

        if (!_enemyStatus || !playerStatus) return;

        var distance = Vector3.Distance(transform.position, _playerGameObject.transform.position);
        if (!(distance <= MeleeRadius)) return;
        playerStatus.TakeDamage(_enemyStatus.Damage);
    }
}