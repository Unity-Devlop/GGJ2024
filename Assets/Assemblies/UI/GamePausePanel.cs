using System;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class GamePausePanel : UIPanel
    {
        [SerializeField, UIBind] private Button resumeButton;
        [SerializeField, UIBind] private Button backToHomeButton;
        [SerializeField,UIBind] private Button tutorialButton;
        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeButtonClick);
            backToHomeButton.onClick.AddListener(OnBackToHomeButtonClick);
            tutorialButton.onClick.AddListener(OnTutorialButtonClick);
        }

        private void OnTutorialButtonClick()
        {
            UIRoot.Singleton.OpenPanel<TutorialPanel>();
        }

        private void OnBackToHomeButtonClick()
        {
            GlobalManager.Singleton.ToHome();
            CloseSelf();    
        }

        private void OnResumeButtonClick()
        {
            GameManager.Singleton.Resume();
            CloseSelf();
        }

        public override void OnOpened()
        {
            base.OnOpened();
            Time.timeScale = 0;
            AudioManager.Singleton.StopBGM();
        }

        public override void OnClosed()
        {
            base.OnClosed();
            Time.timeScale = 1;
            AudioManager.Singleton.PlayBGM(GameManager.Singleton.globalConfig.gameBGM);
        }
    }
}