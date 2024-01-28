using UnityEngine;
using UnityEngine.Video;
using UnityToolkit;

namespace GGJ2024
{
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "GGJ2024/GlobalConfig")]
    public class GlobalConfig : ScriptableObject, IConfig
    {
        public GameObject bodyHitEffectPrefab;
        public GameObject noseHitEffectPrefab;

        
        public AudioClip playerBeHitClip;
        public AudioClip playerDeadClip;
        public AudioClip playerBirthClip;
        
        public AudioClip playerCompleteDeadClip;
        
        public RuntimeAnimatorController p1Controller;
        public RuntimeAnimatorController p2Controller;
        public AudioClip countDownClip;
        public AudioClip countOverClip;
        
        public AudioClip homeBGM;
        
        public AudioClip gameBGM;
        public AudioClip teleportClip;
        public AudioClip stopWallClip;

        // public VideoClip genshinClip;
    }
}