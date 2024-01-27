using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class StopWall : MonoBehaviour
    {
        public Vector2 targetVelocity;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) {
                Rigidbody2D rg = collision.gameObject.GetComponent<Rigidbody2D>();
                rg.velocity = targetVelocity;
                
            }
        }
    }
}
