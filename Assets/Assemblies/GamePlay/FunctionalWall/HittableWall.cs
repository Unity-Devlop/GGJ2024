using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024
{
    [RequireComponent(typeof(Collider2D))]

    
    public class HittableWall : MonoBehaviour
    {
        public int MaxhitCount = 3;
       
        public GameObject shower;
        public AudioClip clip;

       

        private int hitCount = 0;

        public float minTimeBetweenTriggers = 0.5f;
        private float timer = 0f;

        private Stack<Sprite> spriteStack = new Stack<Sprite>();
        public Sprite[] sprites;

        private void Awake()
        {


            
            for (int i = 0; i < sprites.Length; i++)
            {
                spriteStack.Push(sprites[i]);
            }
            
            SpriteRenderer rd = shower.GetComponent<SpriteRenderer>();

            rd.sprite = spriteStack.Pop();
  

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
           
            if (collision.gameObject.CompareTag("Player"))
            {
       
                hitCount++;
                StartCoroutine(showMap());

            }

            if (hitCount > MaxhitCount)
            {
                Destroy(gameObject);
            }

            OnhitPlay();

        }


        private void Update()
        {
            timer -= Time.deltaTime;
        }
        /*
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
        */
        IEnumerator showMap() {

            if (spriteStack.Count != 0)
            {
                Sprite map = spriteStack.Pop();
                if (shower.GetComponent<SpriteRenderer>() != null)
                {
                    SpriteRenderer rd = shower.GetComponent<SpriteRenderer>();
                    rd.sprite = map;
                }


            }
            else {

                if (shower.GetComponent<SpriteRenderer>() != null)
                {
                    SpriteRenderer rd = shower.GetComponent<SpriteRenderer>();
                    rd.sprite = null;
                }
            }

            yield return null;
        
        }

        private void OnhitPlay() {
            AudioManager.Singleton.Play(clip, transform.position, Quaternion.identity);
        }
        
    }
}
