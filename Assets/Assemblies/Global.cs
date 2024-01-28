using UnityEngine;

namespace GGJ2024
{
    public static class Global
    {
        public static readonly int playerMoveAnim = Animator.StringToHash("move");
        public static readonly int playerIdleAnim = Animator.StringToHash("idle");
        public static readonly int playerHitAnim = Animator.StringToHash("playerHit");
        public static readonly int noseHitAnim = Animator.StringToHash("noseHit");
        
        public static readonly int oldManFallAnim = Animator.StringToHash("fall");
        
        public static readonly int gameStartAnim = Animator.StringToHash("start");
        public static string enterTimes = "enterTimes";
    }
}