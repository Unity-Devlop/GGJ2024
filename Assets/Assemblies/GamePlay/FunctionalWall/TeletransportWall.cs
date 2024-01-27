using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class TeletransportWall : MonoBehaviour
    {

        public Transform exitPortal;
        public GameObject targetExitWall;

        private bool triggerable = true;
        private bool cantriggerTarget = false;

        public float triggerInterval = 1f;
        private float timer = 0.0f;

        private void OnTriggerEnter2D(Collider2D collision)
           
        {
           
            if (collision.CompareTag("Player") && triggerable) {
                TeleportPlayer(collision.transform);
                triggerable = false;
                cantriggerTarget = false;
            }
            
        }
        private void Update()
        {
            if (timer > triggerInterval) {
                timer = 0.0f;
                cantriggerTarget = true;
                
            }
            timer += Time.deltaTime;
            Collider2D collider2D = targetExitWall.GetComponent<BoxCollider2D>();
            collider2D.isTrigger = cantriggerTarget;



        }
        private void FixedUpdate()
        {
            if (!triggerable)
            {
                triggerable = true;
            }
        }


        private void TeleportPlayer(Transform playerTransform)
        {
            // 入口的相对位置和旋转
            Vector3 relativePosition = transform.InverseTransformPoint(playerTransform.position);
            Quaternion relativeRotation = Quaternion.Inverse(transform.rotation) * playerTransform.rotation;

            // 出口的位置和旋转
            playerTransform.position = exitPortal.TransformPoint(relativePosition);
            playerTransform.rotation = exitPortal.rotation * relativeRotation;


        }
    }
}
