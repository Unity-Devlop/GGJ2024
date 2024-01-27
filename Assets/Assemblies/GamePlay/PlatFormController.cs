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
        private new Rigidbody2D rigidbody2D;

        public float moveSpeed = 100f;



        private bool movingUp = true;


        void Awake()
        {


            rigidbody2D = GetComponent<Rigidbody2D>();






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

                rigidbody2D.velocity = new Vector2(0f, movement);
            } else {

                rigidbody2D.velocity = new Vector2(0f, -movement);
            }

            if (transform.position.y > Mathf.Max(startPos.position.y, endPos.position.y)) {
                movingUp = false;

            } else if (transform.position.y < Mathf.Min(startPos.position.y, endPos.position.y)) {
                movingUp = true;
            }
        }





    }
}
