using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class PlayerHUD : MonoBehaviour,IUIComponent
    {
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private Image _headIcon;
        [SerializeField] private Image lifeIcon;
        private Player _player;
        public void Bind(Player player)
        {
            _player = player;
            player.currentHealth.Register(OnHealthChanged);
            OnHealthChanged(player.currentHealth);
        }

        private void OnHealthChanged(Property<int> obj)
        {
            // Debug.Log($"OnHealthChanged:{obj.Value}");
            switch (_player.playerEnum)
            {
                case PlayerEnum.P1:
                    _lifeText.text = $"x{obj.Value}";
                    break;
                case PlayerEnum.P2:
                    _lifeText.text = $"{obj.Value}x";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Unbind()
        {
            // _player.currentHealth.UnRegister(OnHealthChanged);
        }
    }
}