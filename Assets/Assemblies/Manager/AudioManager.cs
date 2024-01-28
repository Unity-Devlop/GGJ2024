using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioSource _global;
        public AudioClip gameBGM;
        public AudioClip btnClickSound; // todo

        public GameObject audioPrefab;

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

        public void PlayAtCamera(AudioClip clip)
        {
            GameObject audioGo = Instantiate(audioPrefab, GlobalManager.Singleton.transform.position,
                Quaternion.identity);
            audioGo.GetComponent<AudioSource>().clip = clip;
            audioGo.GetComponent<AudioSource>().Play();
            Destroy(audioGo, clip.length);
        }

        public void Play(AudioClip clip, Vector3 position, Quaternion quaternion)
        {
            GameObject audioGo = Instantiate(audioPrefab, position, quaternion);
            audioGo.GetComponent<AudioSource>().clip = clip;
            audioGo.GetComponent<AudioSource>().Play();
            Destroy(audioGo, clip.length);
        }

        public void ButtonClick()
        {
            GameObject audioGo = Instantiate(audioPrefab, GlobalManager.Singleton.transform.position,
                Quaternion.identity);
            audioGo.GetComponent<AudioSource>().clip = btnClickSound;
            audioGo.GetComponent<AudioSource>().Play();
            Destroy(audioGo, btnClickSound.length);
        }
    }
}