using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class MemberInfoPanel : UIPanel
    {
        private void Awake()
        {
            InputManager.Singleton.input.Global.Esc.performed += OnEsc;
        }

        private void Update()
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                OnEsc(default);
            }
        }

        private void OnEsc(InputAction.CallbackContext obj)
        {
            CloseSelf();
        }
    }
}