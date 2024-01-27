using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class HitEffect : MonoBehaviour
    {
        public float lifeTime = 0.5f;
        public float currentLifeTime = 0;

        private void Update()
        {
            currentLifeTime += Time.deltaTime;
            if (currentLifeTime >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}