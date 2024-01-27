using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityToolkit;

namespace GGJ2024
{
    public class GlobalManager : MonoSingleton<GlobalManager>
    {
        [SerializeField] private Camera _mainCamera;
        public LayerMask hittableLayer;

        protected override void OnInit()
        {

            // UIRoot.Singleton.OpenPanel<HomePanel>();
        }

        protected override void OnDispose()
        {

        }

        public static Vector3 ScreenToWorldPoint(Vector3 screenPos)
        {
            return SingletonNullable._mainCamera.ScreenToWorldPoint(screenPos);
        }
    }
}