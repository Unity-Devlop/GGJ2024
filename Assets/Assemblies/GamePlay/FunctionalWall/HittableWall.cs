using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityToolkit;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class HittableWall : MonoBehaviour
    {
        [SerializeField] private int maxHitCount = 3;
        public SpriteRenderer visual;
        public AudioClip clip;
        public float invincibleTime = 0.5f;
        private bool _canHit = true;
        private int _hitCount;
        public float BrokenVelocity => GameManager.Singleton.config.brokenWallVelocity;

        [SerializeField] private Sprite fullSprite;
        [SerializeField] private Sprite brokenSprite;
        [SerializeField] private Sprite brokenSprite2;


        private void Awake()
        {
            visual.sprite = fullSprite;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_canHit) return;

            //判断碰撞的是player且玩家速度的模大于阈值
            if (collision.gameObject.CompareTag("Player") &&
                collision.rigidbody.velocity.magnitude > BrokenVelocity)
            {
                _hitCount++;
                UpdateVisual();
                _canHit = false;
                //播放音乐
                AudioManager.Singleton.Play(clip, transform.position, Quaternion.identity);
                Timer.Register(invincibleTime, () => { _canHit = true; });
            }

            //判断受击次数
            if (_hitCount > maxHitCount)
            {
                Destroy(gameObject);
                Destroy(visual.gameObject);
            }
        }

        private void UpdateVisual()
        {
            if (_hitCount > maxHitCount / 3 * 1)
            {
                visual.sprite = brokenSprite;
            }

            if (_hitCount > maxHitCount / 3 * 2)
            {
                visual.sprite = brokenSprite2;
            }
        }
    }
}