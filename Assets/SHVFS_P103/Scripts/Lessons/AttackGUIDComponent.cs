using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGUIDComponent : MonoBehaviour
{
    public Guid GUID;
    private void Awake()
    {
        GUID = Guid.NewGuid();
    }
}
