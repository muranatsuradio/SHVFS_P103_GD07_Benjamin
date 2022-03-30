using System;
using System.Collections;
using System.Collections.Generic;
using SHVFS_P103.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUISystem : Singleton<ButtonUISystem>
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        SetGamePause();
    }

    public void SetGamePause()
    {
        if (GameUISystem.Instance.GetUIState() == GameUIState.Pause)
        {
            GameUISystem.Instance.SetUIState(GameUIState.Normal);
            Time.timeScale = 1;
        }
        else
        {
            GameUISystem.Instance.SetUIState(GameUIState.Pause);
            Time.timeScale = 0;
        }
    }
}