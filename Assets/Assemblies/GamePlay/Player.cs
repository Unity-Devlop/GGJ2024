using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor.Animations;
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

        private SpriteRenderer _body;

        private Animator _animator;

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
            _body = transform.Find("Body").GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            AnimatorController controller;
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    controller = GameManager.Singleton.p1Controller;
                    break;
                case PlayerEnum.P2:
                    controller = GameManager.Singleton.p2Controller;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _animator.runtimeAnimatorController = controller;
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

            UpdateAnim();
        }

        private void UpdateAnim()
        {
            int curAnimHash = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            
            if (_rb2D.velocity.magnitude > GameManager.Singleton.config.stopVelocity &&
                curAnimHash != Global.playerMoveAnim)
            {
                // 如果当前动画不是 move 则切换到 move
                // Debug.Log($"{playerEnum}:Move");
                _animator.SetBool(Global.playerIdleAnim, false);
                _animator.SetBool(Global.playerMoveAnim, true); // 速度够大则 move
            }
            

            if (_rb2D.velocity.magnitude <= GameManager.Singleton.config.stopVelocity &&
                curAnimHash != Global.playerIdleAnim)
            {
                _animator.SetBool(Global.playerMoveAnim,false);
                _animator.SetBool(Global.playerIdleAnim,true); // 速度太小则 idle
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // todo 自己攻击时要不要闭眼睛
            if (other.collider.CompareTag("Player"))
            {
                _animator.SetTrigger(Global.playerHitAnim);
            }

            if (other.collider.CompareTag("NoseCollider"))
            {
                _animator.SetTrigger(Global.noseHitAnim);
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
            Color origin = _body.color;
            Color newColor = origin;
            newColor.a = 0.5f;
            _body.color = newColor;
            // todo 闪烁以表示无敌
            Timer.Register(config.invincibleTime, () =>
            {
                _state = PlayerState.Normal;
                _body.color = origin;
            }, onUpdate: f =>
            {
                if (f % 0.2f < 0.1f)
                {
                    _body.color = origin;
                }
                else
                {
                    _body.color = newColor;
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

        public void OnBeNoseAttack(Vector2 force, Vector3 pos)
        {
            GameObject effectGo = GameObject.Instantiate(GameManager.Singleton.bodyHitEffectPrefab, pos, Quaternion.identity);
            effectGo.GetComponent<HitEffect>().SetLifeTime(GameManager.Singleton.config.bodyHitEffectLifeTime);

            AudioManager.Singleton.Play(GameManager.Singleton.playerBeHitClip, transform.position, transform.rotation);
            
            // Debug.Log($"{playerEnum}:NoseAttack , force:{force}");
            // Debug.Log($"{playerEnum}:NoseAttack , force:{force}");
            _rb2D.AddForce(force, ForceMode2D.Impulse);
        }
    }
}