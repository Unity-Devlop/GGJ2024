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
        public float invincibleTime = 0.5f;
        private float invincibleTimer = 0f;



        private int hitCount = 0;

        [Header ("�ܻ�ʱ��ǽ�𻵵��ٶ���ֵ")]
        public float hittedVelocity = 10f;



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
            
            if (invincibleTimer > invincibleTime) {
                //�ж���ײ����player������ٶȵ�ģ������ֵ
                if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > hittedVelocity)
                {
                    
                    StartCoroutine(showMap());
                    hitCount++;

                    //��������
                    OnhitPlay();

                    //��ʱ����0
                    invincibleTimer = 0f;


                }

                //�ж��ܻ�����
                if (hitCount > MaxhitCount)
                {
                    Destroy(gameObject);
                }



                
                
            }


        }


        private void Update()
        {

            invincibleTimer += Time.deltaTime;
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

        //�ܻ����ͼ�仯
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
