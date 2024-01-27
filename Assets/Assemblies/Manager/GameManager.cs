using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public GameState gameState = GameState.Waiting;
        [field: SerializeField] public Transform p1SpawnPoint { get; private set; }
        [field: SerializeField] public Transform p2SpawnPoint { get; private set; }

        [field: SerializeField] public Player p1 { get; private set; }
        [field: SerializeField] public Player p2 { get; private set; }

        public GameConfig config;

        protected override void OnInit()
        {
            // 从持久化路径中读取配置文件
            config = JsonManager.LoadJsonFromStreamingAssets<GameConfig>("Config/GameConfig.json");
            // debug
            GameStart();
        }


#if UNITY_EDITOR
        public void OnValidate()
        {
            JsonManager.SaveJsonToStreamingAssets("Config/GameConfig.json", config);
        }
#endif
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

            // todo 生成玩家
            SpawnPlayer(PlayerEnum.P1);
            SpawnPlayer(PlayerEnum.P2);

            gameState = GameState.Playing;
        }

        private void SpawnPlayer(PlayerEnum playerEnum)
        {
            Player target = null;
            PlayerConfig playerConfig = null;
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    // target = Instantiate(playerPrefab, p1SpawnPoint.position, Quaternion.identity)
                    //     .GetComponent<Player>();
                    target = p1;
                    playerConfig = config.p1Config;
                    break;
                case PlayerEnum.P2:
                    // target = Instantiate(playerPrefab, p2SpawnPoint.position, Quaternion.identity)
                    //     .GetComponent<Player>();
                    target = p2;
                    playerConfig = config.p2Config;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerEnum), playerEnum, null);
            }

            target.SetConfig(playerConfig);
        }

        // [Sirenix.OdinInspector.Button]
        public void GameOver()
        {
            AudioManager.Singleton.StopGameBGM();
            gameState = GameState.GameOver;

            GlobalManager.Singleton.ToHome();
        }

        public void PlayerFailed(PlayerEnum playerEnum)
        {
        }
    }
}