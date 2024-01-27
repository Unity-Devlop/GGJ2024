using System;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class EntryPanel : UIPanel
    {
        [SerializeField, UIBind] private Button startButton;
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            GlobalManager.Singleton.ToGame();
        }
    }
}