using System;
using DG.Tweening;
using Unity.Mathematics;
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
        private Nose _nose;
        private Transform _noseOrigin;
        private Transform _noseDestination;


        // todo 配置文件
        [field: SerializeField] public PlayerEnum playerEnum { get; private set; } = PlayerEnum.P1;
        
        public float moveForce = 10f;
        public float facingLerpTime = 0.1f;
        public float maxVelocityX = 10f;
        public float maxVelocityY = 10f;

        public float noseOutSpeed = 10f;
        public float noseInSpeed = 10f;
        
        public float maxNoseLength = 10f;


        // todo state param
        private bool _isAttacking = false;


        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _nose = transform.Find("Nose").GetComponent<Nose>();
            _noseOrigin = transform.Find("NoseOrigin");
            _noseDestination = transform.Find("NoseDestination");
        }


        private void Update()
        {
            if (GameManager.Singleton.gameState != GameState.Playing)
            {
                return;
            }

            LimitSpeed();

            if (InputManager.Singleton.WasNosePerformThisFrame(playerEnum) && !_isAttacking)
            {
                // todo 改成按住就伸长鼻子?
                Attack();
            }
            else
            {
                Vector2 moveInput = InputManager.Singleton.ReadMoveInput(playerEnum);
                _rb2D.AddForce(moveForce * moveInput);


                // 同时让鼻子朝向慢慢的和移动方向一致
                if (moveInput.magnitude > 0.1f)
                {
                    float moveRotation = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, moveRotation), facingLerpTime);
                }
            }
        }

        private void LimitSpeed()
        {
            Vector2 abs = math.abs(_rb2D.velocity);

            if (abs.x > maxVelocityX)
            {
                // Debug.Log("maxVelocityX");
                int direction = _rb2D.velocity.x > 0 ? 1 : -1;
                _rb2D.velocity = new Vector2(maxVelocityX * direction, _rb2D.velocity.y);
            }

            if (abs.y > maxVelocityY)
            {
                // Debug.Log("maxVelocityY");
                int direction = _rb2D.velocity.y > 0 ? 1 : -1;
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, maxVelocityY * direction);
            }
        }

        public void Attack()
        {
            _isAttacking = true;
            _nose.StartAttack();
            float distance = Vector3.Distance(_noseOrigin.localPosition, _noseDestination.localPosition);
            float outDuration = distance / noseOutSpeed;
            float inDuration = distance / noseInSpeed;
            Transform noseTransform = _nose.transform;
            
            noseTransform.DOLocalMove(_noseDestination.localPosition, outDuration).OnComplete(() =>
            {
                noseTransform.DOLocalMove(_noseOrigin.localPosition, inDuration)
                    .OnComplete(() =>
                    {
                        _isAttacking = false;
                        _nose.CancelAttack();
                    });
            });
        }

        private void OnValidate()
        {
            _noseDestination = transform.Find("NoseDestination");
            _noseOrigin = transform.Find("NoseOrigin");
            
            _noseDestination.localPosition = _noseOrigin.localPosition + Vector3.right * maxNoseLength;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(_noseOrigin.position, _noseDestination.position);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_noseOrigin.position, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_noseDestination.position, 0.1f);
            
        }
    }
}