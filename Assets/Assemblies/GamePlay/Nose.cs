using System;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Nose : MonoBehaviour
    {
        private Player _player;
        private CircleCollider2D _collider2D;
        private bool _attacking;
        public float force => _player.config.noseForce;

        private readonly HashSet<Collider2D> _filter = new HashSet<Collider2D>();

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.enabled = false;
        }

        private void Update()
        {
            if (!_attacking) return;

            PlayerCheck();
            NoseCheck();

        }

        private void PlayerCheck()
        {
            int count = RayCaster2D.OverlapCircle(transform.position, _collider2D.radius, out var playerHits,
                GlobalManager.Singleton.playerLayer);

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = playerHits[i];
                if (_filter.Contains(hit)) continue; // 过滤打到过的
                // 如果鼻子打到玩家
                if (hit.TryGetComponent(out Player player) && player != this._player)
                {
                    Vector2 forceDir = (player.transform.position - transform.position).normalized;
                    player.GetComponent<Rigidbody2D>().AddForce(forceDir * force, ForceMode2D.Impulse);
                    _filter.Add(hit);
                    continue;
                }
            }
        }

        private void NoseCheck()
        {
            int count = RayCaster2D.OverlapCircle(transform.position, _collider2D.radius, out var noseHits,
                GlobalManager.Singleton.noseLayer);
            for (int i = 0; i < count; i++)
            {
                Collider2D hit = noseHits[i];
                if (_filter.Contains(hit)) continue; // 过滤打到过的
                // 如果鼻子打到鼻子
                if (hit.TryGetComponent(out Nose nose) && nose != this)
                {
                    // Debug.Log("Hit Nose");
                    _filter.Add(hit);
                    continue;
                }
            }
        }

        public void StartAttack()
        {
            _attacking = true;
            _collider2D.enabled = true;
            _filter.Clear();
        }
        public void CancelAttack()
        {
            _attacking = false;
            _collider2D.enabled = false;
            _filter.Clear();
        }
    }
}