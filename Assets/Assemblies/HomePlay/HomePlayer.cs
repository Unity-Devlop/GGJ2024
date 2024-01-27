using System;
using UnityEngine;

namespace GGJ2024.HomePlay
{
    public class HomePlayer : MonoBehaviour
    {
        public PlayerEnum playerEnum;
        private bool _isNosePerforming;
        private Transform _nose;

        [SerializeField] private float noseRightAngle = 90f;
        [SerializeField] private float noseLeftAngle = -90f;
        private float _currentNoseAngle;
        private int _nodeRotateDirection = 1;
        public float rotateSpeed = 100f;

        private void Awake()
        {
            _nose = transform.Find("Nose");
        }

        private void Update()
        {
            if (_isNosePerforming) return;
            RotateNose();

            if (InputManager.Singleton.WasNosePerformThisFrame(playerEnum))
            {
                PerformNose();
            }
        }

        private void PerformNose()
        {
            _isNosePerforming = true;
        }

        private void OnNoseReturn()
        {
            _isNosePerforming = false;
        }

        private void RotateNose()
        {
            float deltaAngle = Time.deltaTime * rotateSpeed * _nodeRotateDirection;
            _currentNoseAngle += deltaAngle;
            if (_currentNoseAngle >= noseRightAngle)
            {
                _currentNoseAngle = noseRightAngle;
                _nodeRotateDirection = -1;
            }
            else if (_currentNoseAngle <= noseLeftAngle)
            {
                _currentNoseAngle = noseLeftAngle;
                _nodeRotateDirection = 1;
            }

            // 计算鼻子旋转角度
            // 旋转鼻子
            _nose.rotation = Quaternion.Euler(0, 0, _currentNoseAngle);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            // 以 (0,-1,0) 为正方向
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(0, 0, noseRightAngle) * Vector3.down);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(0, 0, noseLeftAngle) * Vector3.down);
        }
    }
}