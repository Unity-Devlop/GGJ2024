using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2024

{   [RequireComponent (typeof(Rigidbody2D))]
    public class GrandpaController : MonoBehaviour
    {
        private new Camera camera;
        public float waitSeconds = 1.0f;
        private bool IsHit;
        Rect rect;
        Rigidbody2D rg;

        private void Awake()
        {
            GameObject gameObj = GameObject.Find("Main Camera");
            camera = gameObj.GetComponent<Camera>();
            rect = GetRangeOfCamera();
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


        private void Update()
        {   

            
            
        }
        public void FixedUpdate()
        {
            if (!IsHit)
            {   //未被击中时

                rg.velocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
                // Debug.Log(transform.parent.GetComponent<Rigidbody2D>());
                
            }
        
            
        }

        private Rect GetRangeOfCamera()
        {
            float fov = camera.fieldOfView;
            Vector2 cameraPos = camera.transform.position;
            float aspect = camera.aspect;
            float distance = transform.position.z - camera.transform.position.z;

            float height = 2 * distance * Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
            float width = height * camera.aspect;
            Rect rect = new Rect
            {
                xMin = cameraPos.x - width / 2,
                xMax = cameraPos.x + width / 2,
                yMin = cameraPos.y - height / 2,
                yMax = cameraPos.y + height / 2
            };
            return rect;
        }
    }
}
