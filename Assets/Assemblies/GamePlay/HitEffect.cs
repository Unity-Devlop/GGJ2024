using System;
using UnityEngine;
using UnityToolkit;

namespace GGJ2024
{
    public class HitEffect : MonoBehaviour
    {
        private float _lifeTime;
        public float currentLifeTime = 0;

        public void SetLifeTime(float time)
        {
            _lifeTime = time;
        }
        
        private void Update()
        {
            currentLifeTime += Time.deltaTime;
            if (currentLifeTime >= _lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}