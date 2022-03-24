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
    
    private Vector3 _currentPatrolDestination;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Patrol()
    {
        if (_navMeshAgent && _currentPatrolDestination != Vector3.zero)
        {
            _navMeshAgent.SetDestination(_currentPatrolDestination);
        }

        var distance = Vector3.Distance(_currentPatrolDestination, transform.position);
        
        if (distance < MinDistance)
        {
            _currentPatrolDestination = _currentPatrolDestination == EndPatrolPosition.position
                ? StartPatrolPosition.position
                : EndPatrolPosition.position;
        }
    }
}
