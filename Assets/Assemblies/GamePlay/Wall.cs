using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class Wall : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        private float protectTime => GameManager.Singleton.config.normalWallProtectTime;
        private bool canHit = true;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!canHit)return;
            OnHitPlay();
        }

        private void OnHitPlay()
        {
            canHit = false;
            Timer.Register(protectTime, () => { canHit = true; });
            AudioManager.Singleton.Play(clip, transform.position, transform.rotation);
        }
    }
}