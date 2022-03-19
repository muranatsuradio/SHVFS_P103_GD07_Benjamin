using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootComponent : MonoBehaviour
{
    private Animator _animator;

    private bool _isEquipGun = false;

    private static readonly int IsEquipGun = Animator.StringToHash("IsEquipGun");
    private static readonly int IsShoot = Animator.StringToHash("IsShoot");
    private static readonly int IsHoldShoot = Animator.StringToHash("IsHoldShoot");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!PlayerInputSystem.Instance.CanPlayerInput) return;

        if (Input.GetMouseButtonDown(0) && !_isEquipGun)
        {
            _isEquipGun = !_isEquipGun;
            _animator.SetBool(IsEquipGun, _isEquipGun);
        }

        _animator.SetBool(IsHoldShoot, Input.GetMouseButton(0));

        if (!_isEquipGun) return;

        _animator.SetBool(IsShoot, Input.GetMouseButtonDown(0));
    }
}