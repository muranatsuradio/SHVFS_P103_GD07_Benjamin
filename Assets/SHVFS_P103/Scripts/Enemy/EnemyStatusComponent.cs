using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusComponent : BaseStatusComponent
{
    public EnemySimpleAI EnemySimpleAI;
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (EnemySimpleAI)
        {
            EnemySimpleAI.EnemyState = EnemyState.Chase;
        }
    }
}
