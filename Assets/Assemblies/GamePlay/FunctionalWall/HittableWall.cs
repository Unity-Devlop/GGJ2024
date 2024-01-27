using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]
    public class HittableWall : MonoBehaviour
    {
        public int MaxhitCount = 3;


        private int hitCount = 0;

        public float minTimeBetweenTriggers = 0.5f;
        private float timer = 0f;



        private void OnCollisionEnter2D(Collision2D collision)
        {
           
            if (collision.gameObject.CompareTag("Player"))
            {
       
                hitCount++;
                StartCoroutine(FadeOut());
            }

            if (hitCount > MaxhitCount)
            {
                Destroy(gameObject);
            }

        }


        private void Update()
        {
            timer -= Time.deltaTime;
        }

        IEnumerator FadeOut()
        {
            if (GetComponent<Renderer>().material.color.a > 0)
            {
                Color currentColor = GetComponent<Renderer>().material.color;
                float newAlpha = currentColor.a * 0.5f;
                GetComponent<Renderer>().material.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
                yield return null;
            }
        }
        
    }
}
