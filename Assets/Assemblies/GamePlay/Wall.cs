using System;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class Wall : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            
        }
    }
}