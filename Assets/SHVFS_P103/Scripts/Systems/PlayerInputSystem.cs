using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : Singleton<PlayerInputSystem>
{
    public bool CanPlayerInteract = true;
    public bool CanPlayerMove = true;
    public bool CanPlayerRotate = true;
    public bool IsThrowRock = false;

    private void Start()
    {
        CanPlayerInteract = true;
        CanPlayerMove = true;
        CanPlayerRotate = true;
        IsThrowRock = false;
    }
}
