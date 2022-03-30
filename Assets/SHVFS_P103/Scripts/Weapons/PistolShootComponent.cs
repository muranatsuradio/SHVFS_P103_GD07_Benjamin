using System;
using System.Collections;
using System.Collections.Generic;
using SHVFS_P103.Scripts.UI;
using UnityEngine;

public interface IWeapon
{
    public void Shoot();
    public void EmptyShoot();
    public void Reload();
    public bool HasAmmo();
    public int GetCurAmmoCount();
}

public class PistolShootComponent : MonoBehaviour, IWeapon
{
    public int CurrentAmmoCount = MAX_AMMO_COUNT;
    private const int MAX_AMMO_COUNT = 8;
    
    public delegate void ShootEventHandler();
    public static ShootEventHandler OnPlayerShoot;

    public AudioClip ShootClip;
    public AudioClip EmptyShootClip;
    public AudioClip ReloadClip;
    
    private AudioSource _audioSource;
    private PlayerStatusComponent _playerStatusComponent;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _playerStatusComponent = GetComponentInParent<PlayerStatusComponent>();
    }

    public void Shoot()
    {
        if (CurrentAmmoCount <= 0) return;
        
        if (!_audioSource || !ShootClip) return;
        _audioSource.PlayOneShot(ShootClip);
        
        CurrentAmmoCount--;
        CurrentAmmoCount = Mathf.Max(0, CurrentAmmoCount);
        
        var ray = new Ray(transform.position, transform.right);
        
        if (Physics.Raycast(ray, out var hitInfo))
        {
            var enemy = hitInfo.collider.GetComponent<EnemyStatusComponent>();
        
            if (enemy && _playerStatusComponent)
            {
                enemy.TakeDamage(_playerStatusComponent.Damage);
            }
        }
        
        OnPlayerShoot?.Invoke();

        HUDStatusSystem.Instance.UpdateAmmoCount();
    }

    public void EmptyShoot()
    {
        if (!_audioSource || !EmptyShootClip) return;
        _audioSource.PlayOneShot(EmptyShootClip);
    }

    public void Reload()
    {
        if (!_audioSource || !ReloadClip) return;

        if (!_playerStatusComponent) return;

        if (_playerStatusComponent.AmmoInventory <= 0) return;
        
        var ammoInput = Mathf.Min(_playerStatusComponent.AmmoInventory, MAX_AMMO_COUNT);
        
        _playerStatusComponent.AmmoInventory -= ammoInput;
        _playerStatusComponent.AmmoInventory += CurrentAmmoCount;
        
        CurrentAmmoCount = ammoInput;

        _audioSource.PlayOneShot(ReloadClip);
        
        HUDStatusSystem.Instance.UpdateAmmoCount();
    }

    public bool HasAmmo()
    {
        return CurrentAmmoCount > 0;
    }

    public int GetCurAmmoCount()
    {
        return CurrentAmmoCount;
    }
}
