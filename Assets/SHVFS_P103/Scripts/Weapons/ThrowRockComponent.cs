using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRockComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<EnemySimpleAI>()) return;
        Debug.Log("Hello");
        var enemyAI = other.GetComponent<EnemySimpleAI>();

        enemyAI.CurrentDestination = transform.position;
        enemyAI.EnemyState = EnemyState.Attracted;
    }
}
