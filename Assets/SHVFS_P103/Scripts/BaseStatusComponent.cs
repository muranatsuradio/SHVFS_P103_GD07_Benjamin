using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusComponent : MonoBehaviour
{
    public int HealthPoint;
    public int MaxHealthPoint;
    public int Damage;
    public bool IsDead;
    private static readonly int IsHurt = Animator.StringToHash("IsHurt");
    private static readonly int Dead = Animator.StringToHash("IsDead");

    public virtual void TakeDamage(int damage)
    {
        HealthPoint -= damage;

        HealthPoint = Mathf.Max(HealthPoint, 0);

        var animator = GetComponentInChildren<Animator>();

        if (animator)
        {
            animator.SetTrigger(IsHurt);
        }

        if (HealthPoint != 0) return;
        IsDead = true;

        if (animator)
        {
            animator.SetBool(Dead, IsDead);
        }
    }
}