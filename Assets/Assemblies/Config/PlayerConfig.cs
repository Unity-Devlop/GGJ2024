using System;

namespace GGJ2024
{
    [Serializable]
    public class PlayerConfig
    {
        public float moveForce = 10f; // 移动力
        public float normalFacingLerpTime = 0.1f; // 转向插值时间
        public float attackingFacingLerpTime = 0.03f; // 攻击时转向插值时间 

        public int health = 3;
        public float invincibleTime = 1f; // 无敌时间
        
        public float maxVelocityX = 10f; // 最大速度X
        public float maxVelocityY = 10f; // 最大速度Y

        public float noseOutSpeed = 10f; // 鼻子伸出速度
        public float noseInSpeed = 10f; // 鼻子收回速度
        public float maxNoseLength = 3f; // 鼻子最大长度
        public float noseForce = 10f; // 鼻子攻击时的力
        
        public float friction = 0.1f; // 刚体材质 摩擦力
        public float bounce = 0.1f; // 刚体材质 弹力
    }
}