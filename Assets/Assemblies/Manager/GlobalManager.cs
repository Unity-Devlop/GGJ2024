using UnityToolkit;

namespace GGJ2024
{
    public class GlobalManager : MonoSingleton<GlobalManager>
    {
        protected override void OnInit()
        {
            // UIRoot.Singleton.OpenPanel<HomePanel>();
        }
    }
}