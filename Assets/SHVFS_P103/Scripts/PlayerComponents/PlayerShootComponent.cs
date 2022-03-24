using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootComponent : MonoBehaviour
{
    public GameObject PistolGameObject;

    private PistolRayRenderer _pistolRayRenderer;
    private IWeapon _weapon;

    private PlayerMoveComponent _playerMoveComponent;

    private Animator _animator;

    private bool _isEquipGun = false;

    private static readonly int IsEquipGun = Animator.StringToHash("IsEquipGun");
    private static readonly int IsShoot = Animator.StringToHash("IsShoot");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");
    private static readonly int IsUnequipGun = Animator.StringToHash("IsUnequipGun");
    private static readonly int IsReload = Animator.StringToHash("IsReload");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();

        _pistolRayRenderer = PistolGameObject.GetComponent<PistolRayRenderer>();
        _weapon = PistolGameObject.GetComponent<IWeapon>();

        _playerMoveComponent = GetComponent<PlayerMoveComponent>();
    }

    private void Update()
    {
        if (!PlayerInputSystem.Instance.CanPlayerInteract) return;

        // Equip & Unequip
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isEquipGun = !_isEquipGun;

            _playerMoveComponent.MoveSpeed =
                _isEquipGun ? PlayerMoveComponent.MAX_SPEED / 2 : PlayerMoveComponent.MAX_SPEED;
            _animator.SetTrigger(_isEquipGun ? IsEquipGun : IsUnequipGun);
        }

        // Aim
        var isAiming = Input.GetMouseButton(1);
        _animator.SetBool(IsAiming, isAiming);
        _pistolRayRenderer.SetLineRendererActive(isAiming);

        if (!_isEquipGun) return;

        // Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerReload();
        }

        if (!isAiming) return;

        // Shoot
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (_weapon.HasAmmo())
        {
            PlayerShoot();
        }
        else
        {
            PlayerEmptyShoot();
        }
    }

    private void PlayerShoot()
    {
        _animator.SetBool(IsShoot, true);
        _weapon.Shoot();
    }

    private void PlayerReload()
    {
        _animator.SetBool(IsReload, true);
        _weapon.Reload();
    }

    private void PlayerEmptyShoot()
    {
        _weapon.EmptyShoot();
    }
}