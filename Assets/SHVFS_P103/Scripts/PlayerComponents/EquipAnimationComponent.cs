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
        SetHandGunVisible(false);
    }

    public void EquipGun()
    {
        SetHandGunVisible(true);
    }

    public void UnequipGun()
    {
        SetHandGunVisible(false);
    }

    private void SetHandGunVisible(bool isVisible)
    {
        var models = GetComponentsInChildren<MeshRenderer>();

        foreach (var model in models)
        {
            if (!model) continue;
            model.enabled = isVisible;
        }

        OnWaistGun.SetActive(!isVisible);
    }
}