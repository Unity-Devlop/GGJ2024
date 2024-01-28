using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;
using UnityEngine.UI;

namespace GGJ2024
{
    public class KnockOutPanel : UIPanel
    {
        [SerializeField] private Image KnockOutImage;
        [SerializeField] private float fadeInDuration = 1f; // ������ʾ����ʱ��
        [SerializeField] private Image mask;
        public float showingSpeed = 5f;

    
        


        public override void OnOpened()
        {
            base.OnOpened();



            FadeIn();
        }

        // ������ʾ����
        private void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
            StartCoroutine(FillRange());
        }

        // ������ʾ��Э��
        private IEnumerator FadeInCoroutine()
            
        {
            Debug.Log("��������");
            // ��ȡ����ϵ�Image���
            Image blackImage = transform.GetChild(0).GetComponent<Image>();
            if (blackImage == null)
            {
                Debug.LogWarning("Panel has no Image component for fading.");
                yield break;
            }

            float elapsedTime = 0f;
            

            while (elapsedTime < fadeInDuration)
            {
                
                float t = elapsedTime / fadeInDuration;
                Color newColor = blackImage.color;
                newColor.a = Mathf.Lerp(0f, 0.7f, t); // ����͸���ȴ�0��1
                blackImage.color = newColor;
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        //ʵ�ֶ�̬����
        IEnumerator MoveLayerMask() {
            
            RectTransform maskRect = mask.GetComponent<RectTransform>();
            RectTransform KnockOutRect = KnockOutImage.GetComponent<RectTransform>();
            maskRect.sizeDelta = new Vector2(0, KnockOutRect.sizeDelta.y);
            maskRect.anchoredPosition = Vector2.zero;


            while ((maskRect.rect.width) < KnockOutRect.rect.width) {
                
                Debug.Log("maskRect.rect.width: " + maskRect.rect.width);
                Debug.Log("KnockOutRect.rect.width: " + KnockOutRect.rect.width);

                maskRect.sizeDelta = maskRect.sizeDelta  + new Vector2(showingSpeed * Time.deltaTime, 0);
               
                yield return null;


            }
                yield return null;
        }

        //FillAmountʵ��
        IEnumerator FillRange() {

            
            Image image = KnockOutImage.GetComponent<Image>();
            while (image.fillAmount < 1) {
                Debug.Log("Inloop");
                image.fillAmount += showingSpeed * Time.deltaTime;
                yield return null;
            }

            while (image.fillAmount > 0)
            {
                image.fillAmount -= showingSpeed * Time.deltaTime;
                yield return null;
            }

            GameManager.Singleton.GameOver();

            yield return null;


        }


        
    }
}
