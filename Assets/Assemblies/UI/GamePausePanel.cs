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

        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeButtonClick);
            backToHomeButton.onClick.AddListener(OnBackToHomeButtonClick);
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
    }
}