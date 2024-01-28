using TMPro;
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
        [SerializeField] private int gameCountDown = 3;
        [SerializeField] private TextMeshProUGUI _countDownText;

        public override void OnOpened()
        {
            base.OnOpened();

            _p1HUD.gameObject.SetActive(false);
            _p2HUD.gameObject.SetActive(false);
            _countDownText.gameObject.SetActive(true);

            Timer.Register(gameCountDown, () =>
                {
                    _p1HUD.gameObject.SetActive(true);
                    _p2HUD.gameObject.SetActive(true);
                    _countDownText.gameObject.SetActive(false);
                    
                    _p1HUD.Bind(p1);
                    _p2HUD.Bind(p2);
                    
                    GameManager.Singleton.GameStart();
                }, onUpdate: (f) =>
                {
                    
                    int i = gameCountDown - Mathf.FloorToInt(f);
                    if (i == 0)
                    {
                        _countDownText.text = "GO!";
                    }
                    else
                    {
                        _countDownText.text = i.ToString();
                    }
                }
            );
        }


        public override void OnClosed()
        {
            base.OnClosed();
            _p1HUD.Unbind();
            _p2HUD.Unbind();
        }
    }
}