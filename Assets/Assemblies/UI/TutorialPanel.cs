using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class TutorialPanel : UIPanel
    {
        [SerializeField,UIBind]private Button escButton;

        private void Awake()
        {
            escButton.onClick.AddListener(()=>OnEsc(default));
        }
        

        public override void OnOpened()
        {
            base.OnOpened();
            InputManager.Singleton.input.Global.Esc.performed += OnEsc;
        }

        private void OnEsc(InputAction.CallbackContext obj)
        {
            CloseSelf();
        }
    }
}