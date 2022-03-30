using System;
using System.Collections;
using System.Collections.Generic;
using SHVFS_P103.Scripts.UI;
using UnityEngine;

public class PlayerThrowRockComponent : MonoBehaviour
{
    [SerializeField] private GameObject ThrowGameObject;
    [SerializeField] private GameObject ThrowPrefab;
    public float DestroyTime = 0.2f;

    private PlayerStatusComponent _playerStatusComponent;
    private PlayerShootComponent _playerShootComponent;

    private Camera _mainCamera;
    private Animator _animator;

    private static readonly int IsUnequipGun = Animator.StringToHash("IsUnequipGun");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");
    private static readonly int IsThrow = Animator.StringToHash("IsThrow");
    private static readonly int Throw = Animator.StringToHash("Throw");

    private void Start()
    {
        _playerStatusComponent = GetComponent<PlayerStatusComponent>();
        _playerShootComponent = GetComponent<PlayerShootComponent>();

        _mainCamera = Camera.main;
        _animator = GetComponentInChildren<Animator>();

        ThrowGameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_playerStatusComponent || !_playerShootComponent || !_animator || !_mainCamera) return;

        if (_playerStatusComponent.RockInventory <= 0)
        {
            PlayerInputSystem.Instance.IsThrowRock = false;
            _animator.SetBool(IsThrow, PlayerInputSystem.Instance.IsThrowRock);
            if (!ThrowGameObject) return;
            ThrowGameObject.GetComponent<Collider>().enabled = false;
            ThrowGameObject.SetActive(PlayerInputSystem.Instance.IsThrowRock);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerInputSystem.Instance.IsThrowRock = !ThrowGameObject.activeSelf;
        }

        // Ready to throw rock
        if (PlayerInputSystem.Instance.IsThrowRock)
        {
            if (_playerShootComponent.IsEquip)
            {
                _animator.SetBool(IsAiming, false);
                _animator.SetTrigger(IsUnequipGun);
                _playerShootComponent.IsEquip = false;
            }

            _animator.SetBool(IsThrow, PlayerInputSystem.Instance.IsThrowRock);
            ThrowGameObject.SetActive(PlayerInputSystem.Instance.IsThrowRock);

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hitInfo)) return;

            if (!hitInfo.collider.CompareTag("Ground")) return;

            ThrowGameObject.transform.position = hitInfo.point;

            if (!Input.GetMouseButtonDown(0)) return;

            _animator.SetTrigger(Throw);

            var throwGameObject = Instantiate(ThrowPrefab, ThrowGameObject.transform.position, Quaternion.identity);
            Destroy(throwGameObject, DestroyTime);
            
            _playerStatusComponent.RockInventory--;
            HUDStatusSystem.Instance.UpdateRockCount();
            
            PlayerInputSystem.Instance.IsThrowRock = false;
        }
        else
        {
            _animator.SetBool(IsThrow, PlayerInputSystem.Instance.IsThrowRock);

            if (!ThrowGameObject) return;
            ThrowGameObject.GetComponent<Collider>().enabled = false;
            ThrowGameObject.SetActive(PlayerInputSystem.Instance.IsThrowRock);
        }
    }
}