using System;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class Wall : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnHitPlay();
        }

        private void OnHitPlay()
        {
            AudioManager.Singleton.Play(clip, transform.position, transform.rotation);
        }
    }
}