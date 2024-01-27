﻿using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
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

        [field: SerializeField] public GameConfig config { get; private set; }

        public GameObject bodyHitEffectPrefab;
        public GameObject noseHitEffectPrefab;

        // todo config save
        public AudioClip playerBeHitClip;
        public AudioClip playerDeadClip;
        public AudioClip playerBirthClip;
        public AnimatorController p1Controller;
        public AnimatorController p2Controller;

        protected override void OnInit()
        {
            // 从持久化路径中读取配置文件
            // config = JsonManager.LoadJsonFromStreamingAssets<GameConfig>("Config/GameConfig.json");
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

            // todo 生成玩家
            SpawnPlayer(PlayerEnum.P1);
            SpawnPlayer(PlayerEnum.P2);

            gameState = GameState.Playing;
        }

        private void SpawnPlayer(PlayerEnum playerEnum)
        {
            GetPlayer(playerEnum, out Player target, out PlayerConfig playerConfig);
            target.SetConfig(playerConfig);
        }

        private void GetPlayer(PlayerEnum playerEnum, out Player player, out PlayerConfig playerConfig)
        {
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    // target = Instantiate(playerPrefab, p1SpawnPoint.position, Quaternion.identity)
                    //     .GetComponent<Player>();
                    player = p1;
                    playerConfig = config.p1Config;
                    break;
                case PlayerEnum.P2:
                    // target = Instantiate(playerPrefab, p2SpawnPoint.position, Quaternion.identity)
                    //     .GetComponent<Player>();
                    player = p2;
                    playerConfig = config.p2Config;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerEnum), playerEnum, null);
            }
        }

        [Sirenix.OdinInspector.Button]
        public void GameOver()
        {
            AudioManager.Singleton.StopGameBGM();
            gameState = GameState.GameOver;

            UIRoot.Singleton.OpenPanel<GameOverPanel>();
        }

        public void PlayerFailed(PlayerEnum playerEnum)
        {
            GetPlayer(playerEnum, out Player target, out PlayerConfig playerConfig);

            AudioManager.Singleton.Play(playerDeadClip, target.transform.position, target.transform.rotation);

            target.gameObject.SetActive(false); // todo

            if (target.currentHealth.Value <= 0)
            {
                // GameOver();
                return;
            }

            target.currentHealth.Value--;
            target.SetInvincible();
            Vector3 spawnPoint;
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    spawnPoint = p1SpawnPoint.position;
                    break;
                case PlayerEnum.P2:
                    spawnPoint = p2SpawnPoint.position;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerEnum), playerEnum, null);
            }

            Timer.Register(playerConfig.reSpawnTime, () =>
            {
                target.gameObject.SetActive(true);
                target.transform.position = spawnPoint;
                AudioManager.Singleton.Play(playerBirthClip, target.transform.position, target.transform.rotation);
            });
        }

// #if UNITY_EDITOR
//         public void OnValidate()
//         {
//             JsonManager.SaveJsonToStreamingAssets("Config/GameConfig.json", config);
//         }
// #endif
    }
}