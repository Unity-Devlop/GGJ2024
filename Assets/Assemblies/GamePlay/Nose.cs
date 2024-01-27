using System;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class Nose : MonoBehaviour
    {
        private Player _player;
        public float radius = 1f;
        private bool _attacking;
        public float force = 10f;

        private readonly HashSet<Collider2D> _filter = new HashSet<Collider2D>();
        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }


        // todo 鼻子伸长

        // todo 鼻子和鼻子进行 格挡/弹反

        private void Update()
        {
            if (!_attacking) return;
            int count = RayCaster2D.OverlapCircle(transform.position, radius, out var hits,
                GlobalManager.Singleton.hittableLayer);

            for (int i = 0; i < count; i++)
            {
                Collider2D hit = hits[i];
                if (_filter.Contains(hit)) continue;// 过滤打到过的
                // 如果鼻子打到玩家
                if (hit.TryGetComponent(out Player player) && player != this._player)
                {
                    Vector2 forceDir = (player.transform.position - transform.position).normalized;
                    player.GetComponent<Rigidbody2D>().AddForce(forceDir * force, ForceMode2D.Impulse);
                    continue;
                }
                _filter.Add(hit);
                
            }
        }
        
        public void StartAttack()
        {
            _attacking = true;
            _filter.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public void CancelAttack()
        {
            _attacking = false;
            _filter.Clear();
        }
    }
}