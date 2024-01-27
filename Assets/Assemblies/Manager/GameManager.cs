using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public enum GameState
    {
        Playing,
        Waiting,
        Pause,
        GameOver
    }
    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState gameState = GameState.Waiting;
        protected override void OnInit()
        {
            // debug
            GameStart();
        }

        private void Update()
        {
            if (InputManager.Singleton.input.Global.Esc.WasPerformedThisFrame() && gameState == GameState.Playing)
            {
                Pause();
            }
        }
        
        public void Resume()
        {
            UIRoot.Singleton.ClosePanel<GamePausePanel>();
            gameState = GameState.Playing;
            Time.timeScale = 1;
        }

        public void Pause()
        {
            UIRoot.Singleton.OpenPanel<GamePausePanel>();
            gameState = GameState.Pause;
            Time.timeScale = 0;
        }

        public void GameStart()
        {
            AudioManager.Singleton.PlayGameBGM();
            gameState = GameState.Playing;
        }

        [Sirenix.OdinInspector.Button]
        public void GameOver()
        {
            AudioManager.Singleton.StopGameBGM();
            gameState = GameState.GameOver;
            
            GlobalManager.Singleton.ToHome();
        }
    }
}