using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityToolkit;

namespace GGJ2024
{
    public class GlobalManager : MonoSingleton<GlobalManager>
    {
        [SerializeField] private Camera _mainCamera;
        protected override bool DontDestroyOnLoad() => true;
        [field: SerializeField] public LayerMask playerLayer { get; private set; }
        [field: SerializeField] public LayerMask noseLayer { get; private set; }

        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<EntryPanel>();
        }

        protected override void OnDispose()
        {
        }

        public void EnterGame()
        {
            SceneManager.LoadScene("Game");
            UIRoot.Singleton.CloseAll();
        }
        
        public static Vector3 ScreenToWorldPoint(Vector3 screenPos)
        {
            return SingletonNullable._mainCamera.ScreenToWorldPoint(screenPos);
        }
        
        public void BackToHome()
        {
            
            SceneManager.LoadScene("Home");
            UIRoot.Singleton.OpenPanel<EntryPanel>();
        }
    }
}