using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationComponent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
    private static readonly int VerticalInput = Animator.StringToHash("VerticalInput");

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat(VerticalInput, Mathf.Abs(_navMeshAgent.velocity.z));
        _animator.SetFloat(HorizontalInput, Mathf.Abs(_navMeshAgent.velocity.x));
    }
}