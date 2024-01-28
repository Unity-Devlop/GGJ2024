using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class TutorialPanel : UIPanel
    {
        [SerializeField, UIBind] private Button rightButton;
        [SerializeField, UIBind] private Button leftButton;
        [SerializeField, UIBind] private Image _image1;
        [SerializeField, UIBind] private Image _image2;
        private void Awake()
        {
            rightButton.onClick.AddListener(OnRightClick);
            leftButton.onClick.AddListener(OnLeftClick);
        }

        private void OnRightClick()
        {
            _image1.gameObject.SetActive(false);
            _image2.gameObject.SetActive(true);
        }

        private void OnLeftClick()
        {
            _image1.gameObject.SetActive(true);
            _image2.gameObject.SetActive(false);
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