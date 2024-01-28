#define DEV

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolkit;

namespace GGJ2024
{
    public class GlobalManager : MonoSingleton<GlobalManager>
    {
        [field:SerializeField] public Camera MainCamera { get; private set; }
        protected override bool DontDestroyOnLoad() => true;
        [field: SerializeField] public LayerMask playerLayer { get; private set; }
        [field: SerializeField] public LayerMask noseLayer { get; private set; }

        protected override void OnInit()
        {
            ToHome();
        }

        public void ToGame()
        {
            int enterTimes = PlayerPrefs.GetInt(Global.enterTimes);
            if (enterTimes == 0)
            {
                // todo 等待教程页关闭
            }
            else
            {
                UIRoot.Singleton.CloseAll();
                SceneManager.LoadScene("Game");
            }
            
            PlayerPrefs.SetInt(Global.enterTimes, enterTimes + 1);
        }
        
        public static Vector3 ScreenToWorldPoint(Vector3 screenPos)
        {
            return SingletonNullable.MainCamera.ScreenToWorldPoint(screenPos);
        }

        public void ToHome()
        {
            
            UIRoot.Singleton.CloseAll();
            SceneManager.LoadScene("Home");
            UIRoot.Singleton.OpenPanel<HomePanel>();
        }
        
        public Rect GetRangeOfCamera2D()
        {
            Vector2 cameraPos = MainCamera.transform.position;
            float distance = transform.position.z - MainCamera.transform.position.z;

            float height = 2 * distance * Mathf.Tan(MainCamera.fieldOfView / 2 * Mathf.Deg2Rad);
            float width = height * MainCamera.aspect;
            Rect rect = new Rect
            {
                xMin = cameraPos.x - width / 2,
                xMax = cameraPos.x + width / 2,
                yMin = cameraPos.y - height / 2,
                yMax = cameraPos.y + height / 2
            };
            return rect;
        }
    }
}