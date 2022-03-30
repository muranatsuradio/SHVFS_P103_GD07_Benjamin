using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SHVFS_P103.Scripts.UI
{
    public enum GameUIState
    {
        Normal,
        Pause,
        Victory,
        Lost,
    }

    [System.Serializable]
    public struct GameUI
    {
        public GameUIState _uiState;
        public GameObject _uiGameObject;
    }
    
    public class GameUISystem : Singleton<GameUISystem>
    {
        public List<GameUI> GameUIStruct = new List<GameUI>();

        public GameUIState CurrentUIState = GameUIState.Normal;

        private void Start()
        {
            SetUIState(GameUIState.Normal);
        }

        public void SetUIState(GameUIState newUIState)
        {
            var previousUI = GameUIStruct.FirstOrDefault(gameUI => gameUI._uiState == CurrentUIState);
            var newUI = GameUIStruct.FirstOrDefault(gameUI => gameUI._uiState == newUIState);
            
            previousUI._uiGameObject.SetActive(false);
            newUI._uiGameObject.SetActive(true);
            
            CurrentUIState = newUIState;
        }

        public GameUIState GetUIState()
        {
            return CurrentUIState;
        }
    }
}
