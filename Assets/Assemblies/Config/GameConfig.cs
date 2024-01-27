using System;

namespace GGJ2024
{
    [Serializable]
    public class GameConfig
    {
        public PlayerConfig p1Config;
        public PlayerConfig p2Config;

        public float stopVelocity = 0.3f;
        
        
    }
}