using System;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace GGJ2024
{
    public class HomePanel : UIPanel
    {
        [SerializeField, UIBind] private Button startButton;
        private RectTransform btnRectTransform => startButton.transform as RectTransform;
        [SerializeField] private RectTransform buttonPos1;
        [SerializeField] private RectTransform buttonPos2;
        [SerializeField] private Image dogImage;
        [SerializeField] private int curBtnCnt = 0;
        [SerializeField] private Sprite dog1;
        [SerializeField] private Sprite dog2;
        
        [SerializeField] private Button memberInfoButton;
        
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
            memberInfoButton.onClick.AddListener(OnMemberInfoButtonClick);
        }

        private void OnMemberInfoButtonClick()
        {
            UIRoot.Singleton.OpenPanel<MemberInfoPanel>();
        }

        private void OnStartButtonClick()
        {
            curBtnCnt += 1;
            if (curBtnCnt == 1)
            {
                btnRectTransform.localPosition = buttonPos2.localPosition;
                dogImage.sprite = dog2;
            }
            else if (curBtnCnt == 2)
            {
                btnRectTransform.localPosition = buttonPos1.localPosition;
                dogImage.sprite = dog1;
            }
            else if (curBtnCnt == 3)
            {
                Timer.Register(0.3f, ()=>GlobalManager.Singleton.ToGame());
            }
        }

        public override void OnClosed()
        {
            curBtnCnt = 0;
            btnRectTransform.localPosition = buttonPos1.localPosition;
            dogImage.sprite = dog1;
            base.OnClosed();
        }
    }
}