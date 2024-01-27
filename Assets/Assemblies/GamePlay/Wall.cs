using System;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class Wall : MonoBehaviour
    {
        private GameObject audioPrefab;
        public AudioClip clip;
        private void Awake()
        {
            audioPrefab = Resources.Load<GameObject>("Prefabs/Sound");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnhitPlay();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            
        }
        private void OnhitPlay()
        {
            GameObject audioPlayer = Instantiate(audioPrefab, transform.position, Quaternion.identity);
            AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();

        }

    }
}