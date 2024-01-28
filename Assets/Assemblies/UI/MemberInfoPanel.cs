using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class MemberInfoPanel : UIPanel
    {
        [SerializeField, UIBind] private Button escButton;

        private void Awake()
        {
            escButton.onClick.AddListener(CloseSelf);
            InputManager.Singleton.input.Global.Esc.performed+=OnEsc;
        }

        private void OnEsc(InputAction.CallbackContext obj)
        {
            CloseSelf();
        }
    }
}