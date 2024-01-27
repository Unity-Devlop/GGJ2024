#define DEV

using UnityEngine;
using UnityEngine.SceneManagement;
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
            ToHome();
        }

        public void ToGame()
        {
            UIRoot.Singleton.CloseAll();
            SceneManager.LoadScene("Game");
        }
        
        public static Vector3 ScreenToWorldPoint(Vector3 screenPos)
        {
            return SingletonNullable._mainCamera.ScreenToWorldPoint(screenPos);
        }

        public void ToHome()
        {
            
            UIRoot.Singleton.CloseAll();
            SceneManager.LoadScene("Home");
            UIRoot.Singleton.OpenPanel<HomePanel>();
        }
    }
}