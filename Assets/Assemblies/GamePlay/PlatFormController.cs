using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatFormController : MonoBehaviour
    {
        public Transform startPos;
        public Transform endPos;
        private Rigidbody2D _rigidbody2D;
       [field: SerializeField] public PlayerEnum playerEnum { get; private set; }

        private float moveSpeed
        {
            get
            {
                switch (playerEnum)
                {
                    case PlayerEnum.P1:
                        return GameManager.Singleton.config.oldManP1PlatformMoveSpeed;
                    case PlayerEnum.P2:
                        return GameManager.Singleton.config.oldManP2PlatformMoveSpeed;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }



        private bool movingUp = true;


        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {

        }

        private void FixedUpdate()
        {
            MovePlatform();
        }

        void MovePlatform() {

            float movement = moveSpeed * Time.deltaTime;

            if (movingUp) {

                _rigidbody2D.velocity = new Vector2(0f, movement);
            } else {

                _rigidbody2D.velocity = new Vector2(0f, -movement);
            }

            if (transform.position.y > Mathf.Max(startPos.position.y, endPos.position.y)) {
                movingUp = false;

            } else if (transform.position.y < Mathf.Min(startPos.position.y, endPos.position.y)) {
                movingUp = true;
            }
        }





    }
}
