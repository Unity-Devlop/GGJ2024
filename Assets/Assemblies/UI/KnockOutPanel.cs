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
        [SerializeField] private float fadeInDuration = 1f; // 渐变显示的总时间
        [SerializeField] private Image mask;
        public float showingSpeed = 5f;


        public override void OnOpened()
        {
            base.OnOpened();
            AudioManager.Singleton.StopBGM();
            AudioManager.Singleton.PlayAtCamera(GameManager.Singleton.globalConfig.knockAudio);
            FadeIn();
        }

        // 渐变显示方法
        private void FadeIn()
        {
            StartCoroutine(FadeInCoroutine());
            StartCoroutine(FillRange());
        }

        // 渐变显示的协程
        private IEnumerator FadeInCoroutine()
        {
            // Debug.Log("渐变启动");
            // 获取面板上的Image组件
            Image blackImage = transform.GetChild(0).GetComponent<Image>();
            if (blackImage == null)
            {
                // Debug.LogWarning("Panel has no Image component for fading.");
                yield break;
            }

            float elapsedTime = 0f;


            while (elapsedTime < fadeInDuration)
            {
                float t = elapsedTime / fadeInDuration;
                Color newColor = blackImage.color;
                newColor.a = Mathf.Lerp(0f, 0.7f, t); // 渐变透明度从0到1
                blackImage.color = newColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }


        //FillAmount实现
        IEnumerator FillRange()
        {
            Image image = KnockOutImage.GetComponent<Image>();
            while (image.fillAmount < 1)
            {
                // Debug.Log("Inloop");
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