using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioSource _global;
        public AudioClip gameBGM;

        protected override bool DontDestroyOnLoad() => true;


        protected override void OnInit()
        {
            _global = GetComponent<AudioSource>();
        }

        public void PlayGameBGM()
        {
            _global.clip = gameBGM;
            _global.Play();
        }
        
        public void StopGameBGM()
        {
            _global.Stop();
        }
    }
}