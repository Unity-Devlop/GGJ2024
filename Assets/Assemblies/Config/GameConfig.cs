﻿using System;
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

        public float stopWallProtectTime = 1f;

        public float normalWallProtectTime = 1f;

        public float brokenWallVelocity = 1f;

        public float p1OldManDetectRange = 1f;


        public float p2OldManDetectRange = 1f;
        public int wallHitCount = 3;
    }
}