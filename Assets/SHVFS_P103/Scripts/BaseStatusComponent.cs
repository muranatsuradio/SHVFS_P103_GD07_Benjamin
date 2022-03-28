using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatusComponent : MonoBehaviour
{
    public int HealthPoint;
    public int Damage;
    public bool IsDead;

    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;

        HealthPoint = Mathf.Max(HealthPoint, 0);
        
        if (HealthPoint != 0) return;
        IsDead = true;
    }
}
