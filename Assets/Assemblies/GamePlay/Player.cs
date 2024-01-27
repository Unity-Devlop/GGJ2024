using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private Nose _nose;
        private Transform _noseOrigin;
        private Transform _noseDestination;


        // todo 配置文件
        [field: SerializeField] public PlayerEnum playerEnum { get; private set; } = PlayerEnum.P1;

        [field: SerializeField, Sirenix.OdinInspector.ReadOnly]
        private PlayerState _state = PlayerState.Normal;

        [field: SerializeField] public PlayerConfig config { get; private set; }

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
                _rb2D.AddForce(config.moveForce * moveInput);


                // 同时让鼻子朝向慢慢的和移动方向一致
                if (moveInput.magnitude > 0.1f)
                {
                    LerpFacing(Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg);
                }
            }
        }

        private void LerpFacing(float targetRotation)
        {
            float lerpTime;
            if (_isAttacking)
            {
                // Debug.Log($"Attacking Lerp {config.attackingFacingLerpTime}");
                lerpTime = config.attackingFacingLerpTime;
            }
            else
            {
                lerpTime = config.normalFacingLerpTime;
            }

            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetRotation), lerpTime);
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
        }

        public void Attack()
        {
            _isAttacking = true;
            _nose.StartAttack();
            float distance = Vector3.Distance(_noseOrigin.localPosition, _noseDestination.localPosition);
            float outDuration = distance / config.noseOutSpeed;
            float inDuration = distance / config.noseInSpeed;
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
#if UNITY_EDITOR
        private void OnValidate()
        {
            _noseDestination = transform.Find("NoseDestination");
            _noseOrigin = transform.Find("NoseOrigin");

            _noseDestination.localPosition = _noseOrigin.localPosition + Vector3.right * config.maxNoseLength;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(_noseOrigin.position, _noseDestination.position);
            Gizmos.color = Color.green;
            _noseDestination = transform.Find("NoseDestination");
            Gizmos.DrawWireSphere(_noseDestination.position, 0.1f);
        }
#endif
    }
}