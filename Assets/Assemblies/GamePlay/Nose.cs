using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class Nose : MonoBehaviour
    {
        private Player _player;
        private bool _attacking;
        public float force => _player.config.noseForce;

        private SpriteRenderer _visual;
        private CircleCollider2D _physics;

        private readonly HashSet<Collider2D> _filter = new HashSet<Collider2D>();

        private Transform _noseOrigin;


        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _physics = transform.Find("Physics").GetComponent<CircleCollider2D>();
            _visual = transform.Find("Visual").GetComponent<SpriteRenderer>();

            _noseOrigin = transform.Find("NoseOrigin");

            _physics.enabled = false;
        }

        private void Update()
        {
            if (!_attacking) return;

            PlayerCheck();
            NoseCheck();
        }

        private void PlayerCheck()
        {
            int count = RayCaster2D.OverlapCircle(_physics.transform.position, _physics.radius, out var playerHits,
                GlobalManager.Singleton.playerLayer);

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = playerHits[i];
                // Debug.Log(hit.gameObject.name);
                if (_filter.Contains(hit)) continue; // 过滤打到过的
                // 如果鼻子打到玩家
                if (hit.TryGetComponent(out Player player) && player != this._player)
                {
                    Vector2 forceDir = (player.transform.position - transform.position).normalized;
                    // Debug.Log("Hit Player");
                    player.OnBeNoseAttack(forceDir * force, _physics.transform.position);
                    _filter.Add(hit);
                    continue;
                }
            }
        }

        private void NoseCheck()
        {
            int count = RayCaster2D.OverlapCircle(_physics.transform.position, _physics.radius, out var noseHits,
                GlobalManager.Singleton.noseLayer);
            for (int i = 0; i < count; i++)
            {
                Collider2D hit = noseHits[i];
                if (_filter.Contains(hit)) continue; // 过滤打到过的
                // 如果鼻子打到鼻子
                if (hit.TryGetComponent(out Nose nose) && nose != this)
                {
                    // Debug.Log("Hit Nose");
                    nose.OnBeNoseAttack(_physics.transform.position);
                    _filter.Add(hit);
                    continue;
                }
            }
        }

        private void OnBeNoseAttack(Vector3 position)
        {
            GameObject effectGo = Instantiate(GameManager.Singleton.globalConfig.noseHitEffectPrefab, position, Quaternion.identity);
            effectGo.GetComponent<HitEffect>().SetLifeTime(GameManager.Singleton.config.noseHitEffectLifeTime);
        }

        public void CancelAttack()
        {
            _physics.transform.localPosition = _noseOrigin.localPosition;
            _attacking = false;
            _physics.enabled = false;
            _filter.Clear();
        }

        public void StartAttack(float configNoseOutSpeed, float configNoseInSpeed, float maxLength, Action onComplete)
        {
            _physics.transform.localPosition = _noseOrigin.localPosition;
            _attacking = true;
            _physics.enabled = true;
            _filter.Clear();


            Vector2 defaultSize = _visual.size;
            Vector2 maxSize = defaultSize;
            maxSize.x = maxLength;

            // Debug.DrawLine(transform.position, transform.position + (Vector3) (Vector2.right * maxLength), Color.red,
            //     1f);

            float distance = maxSize.x;
            float outDuration = distance / configNoseOutSpeed;
            float inDuration = distance / configNoseInSpeed;


            // var pos = _physics.transform.position;
            // Debug.DrawLine(pos, pos + maxSize.x * _physics.transform.right
            //     , Color.blue,
            //     1f);

            DOTween.To(() => _visual.size, OnSizeUpdate, maxSize, outDuration)
                // .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    DOTween.To(() => _visual.size, OnSizeUpdate, defaultSize, inDuration)
                        // .SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            onComplete?.Invoke();
                            CancelAttack();
                        });
                });
        }

        private void OnSizeUpdate(Vector2 size)
        {
            _visual.size = size;
            var pos = _physics.transform.localPosition;
            pos.x = size.x;
            _physics.transform.localPosition = pos;
        }
    }
}