using System;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class FailedArea : MonoBehaviour
    {
        [field: SerializeField] public PlayerEnum playerEnum { get; private set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out Player player)) return;
            if (playerEnum == player.playerEnum)
            {
                Debug.Log($"Player {playerEnum} failed!");
            }
        }
    }
}