using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerComponent : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    private void Start()
    {
        transform.position = Player.transform.position;
    }

    private void LateUpdate()
    {
        if (!Player) return;

        transform.position = Player.transform.position;
    }
}