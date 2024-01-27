using UnityToolkit;

namespace GGJ2024
{
    public enum GameState
    {
        Playing,
        Waiting,
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

        public void GameStart()
        {
            AudioManager.Singleton.PlayGameBGM();
        }

        public void GameOver()
        {
            AudioManager.Singleton.StopGameBGM();
        }
    }
}