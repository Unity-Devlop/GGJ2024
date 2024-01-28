using System;
using UnityEngine.Serialization;

namespace GGJ2024
{
    [Serializable]
    public class GameConfig
    {
        public PlayerConfig p1Config;
        public PlayerConfig p2Config;

        public float stopVelocity = 0.3f;
        
        public float bodyHitEffectLifeTime = 2f;
        
        public float noseHitEffectLifeTime = 2f;
        
        public float oldManP1PlatformMoveSpeed = 100f;
        
        public float oldManP2PlatformMoveSpeed = 100f;
        
    }
}