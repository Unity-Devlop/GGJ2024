using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class StopWall : MonoBehaviour
    {
        public Vector2 targetVelocity;
        private float protectTime => GameManager.Singleton.config.stopWallProtectTime;
        private bool _canHit;

        private void Awake()
        {
            _canHit = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            if(!_canHit)return;
            Rigidbody2D rg = collision.gameObject.GetComponent<Rigidbody2D>();
            rg.velocity = targetVelocity;
            _canHit = false;
            Timer.Register(protectTime, () => { _canHit = true; });
            AudioManager.Singleton.PlayAtCamera(GameManager.Singleton.globalConfig.stopWallClip);
        }
    }
}
