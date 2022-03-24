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

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
