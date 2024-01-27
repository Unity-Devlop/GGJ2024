using UnityEngine;
using UnityEngine.Video;
using UnityToolkit;

namespace GGJ2024
{
    public class GenshinPanel : UIPanel
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        public void PlayVideo()
        {
            // 从12s开始播放
            _videoPlayer.time = 12;
            _videoPlayer.Play();
        }

        public void StopVideo()
        {
            _videoPlayer.Stop();
        }
    }
}