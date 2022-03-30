using System.Collections;
using System.Collections.Generic;
using SHVFS_P103.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatusComponent : BaseStatusComponent
{
    public int AmmoInventory = 30;
    
    public int RockInventory = MAX_ROCK_COUNT;
    public const int MAX_ROCK_COUNT = 10;

    public float RestartTime = 3f;
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
        HUDStatusSystem.Instance.UpdateHealthPoint();
        
        if (!IsDead) return;

        PlayerInputSystem.Instance.CanPlayerInteract = false;
        PlayerInputSystem.Instance.CanPlayerMove = false;
        PlayerInputSystem.Instance.CanPlayerRotate = false;
        
        GameUISystem.Instance.SetUIState(GameUIState.Lost);
        Invoke(nameof(RestartGame), 3f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}