using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Shoot();
    public void EmptyShoot();
    public void Reload();
    public bool HasAmmo();
}

public class PistolShootComponent : MonoBehaviour, IWeapon
{
    public int CurrentAmmoCount = 16;
    private const int MAX_AMMO_COUNT = 16;
    
    public AudioClip ShootClip;
    public AudioClip EmptyShootClip;
    public AudioClip ReloadClip;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (CurrentAmmoCount <= 0) return;
        
        if (!_audioSource || !ShootClip) return;
        _audioSource.PlayOneShot(ShootClip);
        
        CurrentAmmoCount--;
        CurrentAmmoCount = Mathf.Max(0, CurrentAmmoCount);
    }

    public void EmptyShoot()
    {
        if (!_audioSource || !EmptyShootClip) return;
        _audioSource.PlayOneShot(EmptyShootClip);
    }

    public void Reload()
    {
        if (!_audioSource || !ReloadClip) return;
        _audioSource.PlayOneShot(ReloadClip);
    }

    public bool HasAmmo()
    {
        return CurrentAmmoCount > 0;
    }
}
