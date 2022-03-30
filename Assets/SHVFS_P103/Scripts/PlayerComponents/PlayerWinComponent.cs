using System;
using System.Collections;
using System.Collections.Generic;
using SHVFS_P103.Scripts.UI;
using UnityEngine;

public class PlayerWinComponent : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerStatusComponent>()) return;
        GameUISystem.Instance.SetUIState(GameUIState.Victory);
        Time.timeScale = 0;
    }
}
