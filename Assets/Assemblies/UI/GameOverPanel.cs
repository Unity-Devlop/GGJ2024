using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityToolkit;

namespace GGJ2024
{
    public class GameOverPanel : UIPanel
    {
        [SerializeField, UIBind] private Image mask;
        private Tweener _alphaTweener;
        [SerializeField] private float alphaTime = 3f;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private Vector2Int textureSize = new Vector2Int(854, 480);
        [SerializeField] private AudioClip _deadClip;
        // private Animator _animator;

        // private void Awake()
        // {
        //     // _animator = GetComponent<Animator>();
        // }

        public override void OnOpened()
        {
            base.OnOpened();
            // _animator.enabled = true;
            RenderTexture videoTexture = new RenderTexture(textureSize.x, textureSize.y, 0);
            
            _videoPlayer.targetTexture = videoTexture;
            _videoPlayer.GetComponent<RawImage>().texture = videoTexture;

            AudioManager.Singleton.PlayAtCamera(_deadClip);
            
            Timer.Register(4f, OnAnimKeyFrame);
        }

        private void Update()
        {
            if (!InputManager.Singleton.input.Global.Esc.WasPerformedThisFrame()) return;
            _alphaTweener.Kill();
            _timer?.Cancel();
            GlobalManager.Singleton.ToHome();
        }

        public void OnAnimKeyFrame()
        {
            // Debug.Log("OnAnimKeyFrame");
            // _animator.enabled = false;
            // 透明度渐变
            _alphaTweener = DOTween.ToAlpha(() => mask.color, x => mask.color = x, 1, alphaTime);
            // 原神
            _alphaTweener.onComplete += PlayGenshin;
        }

        private Timer _timer;
        private void PlayGenshin()
        {
            // mask.gameObject.SetActive(false);
            // Debug.Log("PlayGenshin");
            _videoPlayer.gameObject.SetActive(true);
            float start = 0;//13.5f;
            _videoPlayer.time = start;
            _videoPlayer.Play();
            // Debug.Log(_videoPlayer.clip.length);
            _timer = Timer.Register((float)_videoPlayer.clip.length - start, () => { GlobalManager.Singleton.ToHome(); });
        }

        public override void OnClosed()
        {
            _videoPlayer.Stop();
            _videoPlayer.gameObject.SetActive(false);
            _alphaTweener?.Kill();
            _timer?.Cancel();
            // mask.gameObject.SetActive(true);
            mask.color = new Color(1, 1, 1, 0);
            
            base.OnClosed();
        }
    }
}