using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024

{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GrandpaController : MonoBehaviour
    {
        private float radius
        {
            get
            {
                switch (_playerEnum)
                {
                    case PlayerEnum.P1:
                        return GameManager.Singleton.config.p1OldManDetectRange;
                    case PlayerEnum.P2:
                        return GameManager.Singleton.config.p2OldManDetectRange;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private bool _isHit;
        private Rect rect => GlobalManager.Singleton.GetRangeOfCamera2D();
        private Rigidbody2D _rg;
        private PlatFormController _parentPlatFormController;
        private PlayerEnum _playerEnum => _parentPlatFormController.playerEnum;

        private void Awake()
        {
            _parentPlatFormController = GetComponentInParent<PlatFormController>();
            _isHit = false;
            _rg = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            _isHit = true;
            Vector2 playerVelocity = collision.rigidbody.velocity;
            _rg.velocity = playerVelocity;
        }

        private void Update()
        {
            if (!_isHit)
            {
                //未被击中时
                _rg.velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
            }

            float distance = Vector3.Distance(_parentPlatFormController.transform.position, transform.position);
            if (!(distance > radius)) return;
            Destroy(gameObject);
            UIRoot.Singleton.OpenPanel<KnockOutPanel>();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            GameManager gameManager = FindObjectOfType<GameManager>().gameObject.GetComponent<GameManager>();
            float radius;
            PlayerEnum playerEnum = GetComponentInParent<PlatFormController>()
                .playerEnum;
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    radius = gameManager.config.p1OldManDetectRange;
                    break;
                case PlayerEnum.P2:
                    radius = gameManager.config.p2OldManDetectRange;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}