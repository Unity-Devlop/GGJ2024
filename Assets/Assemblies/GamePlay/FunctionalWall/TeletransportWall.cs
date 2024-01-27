using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class TeletransportWall : MonoBehaviour
    {

        public Transform exitPortal;

        private bool triggerable = true;

        private void OnTriggerEnter2D(Collider2D collision)
           
        {
           
            if (collision.CompareTag("Player") && triggerable) {
                TeleportPlayer(collision.transform);
                triggerable = false;
            }
        }
        private void Update()
        {
            if (!triggerable) { 
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
