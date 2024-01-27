using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class GamePanel : UIPanel
    {
        [SerializeField] private PlayerHUD _p1HUD;
        [SerializeField] private PlayerHUD _p2HUD;
        private Player p1 => GameManager.Singleton.p1;
        private Player p2 => GameManager.Singleton.p2;
        public override void OnOpened()
        {
            base.OnOpened();
            _p1HUD.Bind(p1);
            _p2HUD.Bind(p2);
        }


        public override void OnClosed()
        {
            base.OnClosed();
            _p1HUD.Unbind();
            _p2HUD.Unbind();
        }
    }
}