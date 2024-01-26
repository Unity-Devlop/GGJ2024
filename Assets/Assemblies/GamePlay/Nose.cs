using System;
using UnityEngine;

namespace GGJ2024
{
    public class Nose : MonoBehaviour
    {
        private Player _player;
        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }
        
        
        // todo 鼻子伸长
        
        // todo 鼻子和鼻子进行 格挡/弹反
    }
}