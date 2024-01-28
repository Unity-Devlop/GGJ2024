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
        [SerializeField] private Animator _animator;
        
        public override void OnOpened()
        {
            base.OnOpened();
            RenderTexture videoTexture = new RenderTexture(textureSize.x, textureSize.y, 0);
            
            _videoPlayer.targetTexture = videoTexture;
            _videoPlayer.GetComponent<RawImage>().texture = videoTexture;

            // todo 老头动画
            _animator.Play(Global.oldManFallAnim);
            AudioManager.Singleton.PlayAtCamera(_deadClip);
        }

        public void OnAnimKeyFrame()
        {
            // 透明度渐变
            _alphaTweener = DOTween.ToAlpha(() => mask.color, x => mask.color = x, 1, alphaTime);
            // 原神
            _alphaTweener.onComplete += PlayGenshin;
        }

        private void PlayGenshin()
        {
            // mask.gameObject.SetActive(false);
            // Debug.Log("PlayGenshin");
            _videoPlayer.gameObject.SetActive(true);
            float start = 0;//13.5f;
            _videoPlayer.time = start;
            _videoPlayer.Play();
            Timer.Register((float)_videoPlayer.clip.length - start, () => { GlobalManager.Singleton.ToHome(); });
        }

        public override void OnClosed()
        {
            _videoPlayer.Stop();
            _videoPlayer.gameObject.SetActive(false);
            _alphaTweener?.Kill();
            // mask.gameObject.SetActive(true);
            mask.color = new Color(1, 1, 1, 0);
            
            base.OnClosed();
        }
    }
}