using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityToolkit;

namespace GGJ2024
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb2D;

        private Nose _nose;

        // private Transform _noseOrigin;
        // private Transform _noseDestination;
        private SpriteRenderer _visual;

        // todo 配置文件
        [field: SerializeField] public PlayerEnum playerEnum { get; private set; } = PlayerEnum.P1;

        [field: SerializeField] public PlayerConfig config { get; private set; }

        // todo state param
        [field: SerializeField, Sirenix.OdinInspector.ReadOnly]
        private PlayerState _state = PlayerState.Normal;

        public Property<int> currentHealth;

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _nose = transform.Find("Nose").GetComponent<Nose>();
            _visual = GetComponent<SpriteRenderer>();
        }


        private void Update()
        {
            if (GameManager.Singleton.gameState != GameState.Playing)
            {
                return;
            }

            LimitSpeed();

            if (InputManager.Singleton.WasNosePerformThisFrame(playerEnum) && _state != PlayerState.Attacking)
            {
                // todo 改成按住就伸长鼻子?
                Attack();
            }

            Vector2 moveInput = InputManager.Singleton.ReadMoveInput(playerEnum);
            _rb2D.AddForce(config.moveForce * moveInput);

            // 同时让鼻子朝向慢慢的和移动方向一致
            if (moveInput.magnitude > 0.1f)
            {
                LerpFacing(Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg);
            }
        }

        private void LerpFacing(float targetRotation)
        {
            float lerpTime;
            if (_state == PlayerState.Attacking)
            {
                // Debug.Log($"Attacking Lerp {config.attackingFacingLerpTime}");
                lerpTime = config.attackingFacingLerpTime;
            }
            else
            {
                lerpTime = config.normalFacingLerpTime;
            }
            
            Quaternion rotation3D =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), lerpTime);
            // _rb2D.rotation += rotation3D.eulerAngles.z - transform.rotation.eulerAngles.z;
            // tag 用刚体来旋转 而不是 transform 否则物理会出现异常
            _rb2D.MoveRotation(rotation3D.eulerAngles.z);
        }

        private void LimitSpeed()
        {
            Vector2 abs = math.abs(_rb2D.velocity);

            if (abs.x > config.maxVelocityX)
            {
                // Debug.Log("maxVelocityX");
                int direction = _rb2D.velocity.x > 0 ? 1 : -1;
                _rb2D.velocity = new Vector2(config.maxVelocityX * direction, _rb2D.velocity.y);
            }

            if (abs.y > config.maxVelocityY)
            {
                // Debug.Log("maxVelocityY");
                int direction = _rb2D.velocity.y > 0 ? 1 : -1;
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, config.maxVelocityY * direction);
            }
        }

        public void SetConfig(PlayerConfig config)
        {
            this.config = config;
            currentHealth = new Property<int>(config.health, 0, config.health);
        }

        public void SetInvincible()
        {
            _state = PlayerState.Invincible;
            _rb2D.velocity = Vector2.zero;
            _rb2D.angularVelocity = 0;
            Color origin = _visual.color;
            Color newColor = origin;
            newColor.a = 0.5f;
            _visual.color = newColor;
            // todo 闪烁以表示无敌
            Timer.Register(config.invincibleTime, () =>
            {
                _state = PlayerState.Normal;
                _visual.color = origin;
            }, onUpdate: (f) =>
            {
                if (f % 0.2f < 0.1f)
                {
                    _visual.color = origin;
                }
                else
                {
                    _visual.color = newColor;
                }
            });
        }


        public void Attack()
        {
            _state = PlayerState.Attacking;
            _nose.StartAttack(config.noseOutSpeed, config.noseInSpeed, config.maxNoseLength, () =>
            {
                _state = PlayerState.Normal;
                _nose.CancelAttack();
            });
        }
    }
}