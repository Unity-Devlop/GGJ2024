using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2024
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSound : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            AudioManager.Singleton.ButtonClick();
        }
    }
}