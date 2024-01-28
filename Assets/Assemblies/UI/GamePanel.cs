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
        private Animator _animator;

        [SerializeField] private RectTransform countItem;

        private Timer _timer;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void OnOpened()
        {
            base.OnOpened();
            // Debug.Log("GamePanel OnOpened");
            countItem.gameObject.SetActive(true);
            _animator.enabled = true;
            _animator.Play(Global.gameStartAnim);

            // Debug.Log("GamePanel OnOpened");
            
            Timer.Register(3, OnKeyFrame);
            
            // Debug.Log("GamePanel OnOpened");
            _p1HUD.gameObject.SetActive(false);
            _p2HUD.gameObject.SetActive(false);
        }

        public void OnKeyFrame()
        {
            // Debug.Log("OnKeyFrame");
            countItem.gameObject.SetActive(false);
            _animator.enabled = false;
            _p1HUD.gameObject.SetActive(true);
            _p2HUD.gameObject.SetActive(true);

            _p1HUD.Bind(p1);
            _p2HUD.Bind(p2);
            GameManager.Singleton.GameStart();
        }


        public override void OnClosed()
        {
            // Debug.Log("GamePanel OnClosed");
            // _timer?.Cancel();
            // CancelInvoke();
            _p1HUD.Unbind();
            _p2HUD.Unbind();
            base.OnClosed();
        }
    }
}