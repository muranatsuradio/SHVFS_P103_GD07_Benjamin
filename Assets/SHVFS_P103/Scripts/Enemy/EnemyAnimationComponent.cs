using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationComponent : MonoBehaviour
{
    public EnemySimpleAI EnemySimpleAI;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
    private static readonly int VerticalInput = Animator.StringToHash("VerticalInput");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsStunned = Animator.StringToHash("IsStunned");

    private void Start()
    {
        _navMeshAgent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_navMeshAgent) return;

        _animator.SetFloat(VerticalInput, Mathf.Abs(_navMeshAgent.velocity.z));
        _animator.SetFloat(HorizontalInput, Mathf.Abs(_navMeshAgent.velocity.x));

        if (!EnemySimpleAI) return;

        _animator.SetBool(IsStunned, EnemySimpleAI.IsStunned);

        if (!EnemySimpleAI.IsAttacking) return;
        _animator.SetTrigger(IsAttacking);
    }

    public void Attack()
    {
        if (!EnemySimpleAI) return;
        
        EnemySimpleAI.ApplyDamage();
    }
}