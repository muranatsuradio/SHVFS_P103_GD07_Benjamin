using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggrosZoneComponent : MonoBehaviour
{
    public EnemyStatusComponent EnemyStatusComponent;
    public EnemySimpleAI EnemySimpleAI;
    
    private void Start()
    {
        if (EnemyStatusComponent) return;
        
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!EnemyStatusComponent) return;
        
        if (!EnemyStatusComponent.IsDead) return;
        
        gameObject.SetActive(false);

        if (!EnemySimpleAI || !gameObject.activeSelf) return;
        
        transform.localScale = new Vector3(EnemySimpleAI.AggroRadius / 2, EnemySimpleAI.AggroRadius / 2, 1);
    }
}
