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
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");
    private static readonly int IsUnequipGun = Animator.StringToHash("IsUnequipGun");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!PlayerInputSystem.Instance.CanPlayerInteract) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isEquipGun = !_isEquipGun;
            _animator.SetTrigger(_isEquipGun ? IsEquipGun : IsUnequipGun);
        }

        _animator.SetBool(IsAiming, Input.GetMouseButton(1));
        
        if (!_isEquipGun) return;

        _animator.SetBool(IsShoot, Input.GetMouseButtonDown(0));
    }
}