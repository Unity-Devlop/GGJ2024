using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace GGJ2024
{
    public enum PlayerEnum
    {
        P1,
        P2
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private GGJ2024Input _input;
        private Transform _nose;


        // todo 配置文件
        [field: SerializeField] public PlayerEnum playerEnum { get; private set; } = PlayerEnum.P1;
        public float moveForce = 10f;
        public float facingLerpTime = 0.1f;
        public float maxVelocityX = 10f;
        public float maxVelocityY = 10f;
        
        // todo state param
        // private bool _isAttacking = false;
        

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _nose = transform.Find("Nose");
            _input = new GGJ2024Input();
            _input.Enable();
        }

        public Vector2 ReadMoveInput()
        {
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    return _input.Player1.Move.ReadValue<Vector2>();
                case PlayerEnum.P2:
                    return _input.Player2.Move.ReadValue<Vector2>();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool WasNodePerformThisFrame()
        {
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    return _input.Player1.Nose.WasPerformedThisFrame();
                case PlayerEnum.P2:
                    return _input.Player2.Nose.WasPerformedThisFrame();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            if (WasNodePerformThisFrame())
            {
                // todo 改成按住就伸长鼻子?
                Attack();
            }
            else
            {
                Vector2 moveInput = ReadMoveInput();
                _rb2D.AddForce(moveForce * moveInput);
                if (_rb2D.velocity.x > maxVelocityX)
                {
                    _rb2D.velocity = new Vector2(maxVelocityX, _rb2D.velocity.y);
                }

                if (_rb2D.velocity.y > maxVelocityY)
                {
                    _rb2D.velocity = new Vector2(_rb2D.velocity.x, maxVelocityY);
                }

                // 同时让鼻子朝向慢慢的和移动方向一致
                if (moveInput.magnitude > 0.1f)
                {
                    float moveRotation = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, moveRotation), facingLerpTime);
                }
            }
        }

        public void Attack()
        {
            //TODO 伸出鼻子进行攻击
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // todo 如果是对方的鼻子 
            // 1.拿到对方的朝向
            // 2.计算对方给自己的力
            // 3.给自己施加力
        }
    }
}