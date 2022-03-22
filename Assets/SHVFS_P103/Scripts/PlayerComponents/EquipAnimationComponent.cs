using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAnimationComponent : MonoBehaviour
{
    public GameObject OnHandGun;
    public GameObject OnWaistGun;

    private void Start()
    {
        OnHandGun.SetActive(false);
        OnWaistGun.SetActive(true);
    }

    public void EquipGun()
    {
        OnHandGun.SetActive(true);
        OnWaistGun.SetActive(false);
    }

    public void UnequipGun()
    {
        OnHandGun.SetActive(false);
        OnWaistGun.SetActive(true);
    }
}
