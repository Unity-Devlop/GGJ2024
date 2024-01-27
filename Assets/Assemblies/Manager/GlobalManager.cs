using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityToolkit;

namespace GGJ2024
{
    public class GlobalManager : MonoSingleton<GlobalManager>
    {
        [SerializeField] private Camera _mainCamera;
        [field: SerializeField] public LayerMask playerLayer { get; private set; }
        [field: SerializeField] public LayerMask noseLayer { get; private set; }

        protected override void OnInit()
        {
            // UIRoot.Singleton.OpenPanel<HomePanel>();
        }

        protected override void OnDispose()
        {
        }
        
        
        // [Sirenix.OdinInspector.Button]
        public void PlayGenshin()
        {
            GenshinPanel panel= UIRoot.Singleton.OpenPanel<GenshinPanel>();
            panel.PlayVideo();
        }

        public static Vector3 ScreenToWorldPoint(Vector3 screenPos)
        {
            return SingletonNullable._mainCamera.ScreenToWorldPoint(screenPos);
        }
    }
}