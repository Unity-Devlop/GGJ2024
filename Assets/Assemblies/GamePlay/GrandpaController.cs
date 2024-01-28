using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024

{   [RequireComponent (typeof(Rigidbody2D))]
    public class GrandpaController : MonoBehaviour
    {
        public float waitSeconds = 1.0f;
        private bool IsHit;
        private Rect rect => GlobalManager.Singleton.GetRangeOfCamera2D();
        private Rigidbody2D rg;
        private PlatFormController _parentPlatFormController;
        private PlayerEnum _playerEnum=>_parentPlatFormController.playerEnum;
        private void Awake()
        {
            _parentPlatFormController = GetComponentInParent<PlatFormController>();
            IsHit = false;
            rg = GetComponent<Rigidbody2D>();
        }
        private bool IsOutOfScreen()
        {
            /*
            Debug.Log("Transform: " + transform.position.x + " " + transform.position.y);
            Debug.Log("rect: " + rect.xMin + " " + rect.xMax + " " + rect.yMin + " " + rect.yMax);
            */
            if ((transform.position.x < rect.xMin || transform.position.x > rect.xMax) && ( transform.position.y > rect.yMax || transform.position.y < rect.yMin)) {

                //GameManager.Singleton.GameOver();
                
                return true;
            }
            return false;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                IsHit = true;
                Vector2 playerVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                if (playerVelocity == null || playerVelocity == Vector2.zero) {
                    //TODO:exception case

                }
               
                rg.velocity = playerVelocity;
                StartCoroutine(DectectIfInScreen());

            }
        }

        IEnumerator DectectIfInScreen() {
            while (!IsOutOfScreen()) {

                yield return new WaitForSeconds(0.5f);
            
            }

            Destroy(gameObject);
            yield return null;
        
        }

        
        public void FixedUpdate()
        {
            if (!IsHit)
            {   //未被击中时

                rg.velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
                // Debug.Log(transform.parent.GetComponent<Rigidbody2D>());
                
            }
        
            
        }


    }
}
