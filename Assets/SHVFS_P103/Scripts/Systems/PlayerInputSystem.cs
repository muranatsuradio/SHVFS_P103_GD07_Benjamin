using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : Singleton<PlayerInputSystem>
{
    public bool CanPlayerInput = true;

    private void Start()
    {
        CanPlayerInput = true;
    }
}
