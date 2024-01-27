using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityToolkit;

namespace GGJ2024
{
    public class InputManager : MonoSingleton<InputManager>
    {
        protected override bool DontDestroyOnLoad() => true;
        public GGJ2024Input input { get; private set; }
        protected override void OnInit()
        {
            input = new GGJ2024Input();
            input.Enable();
        }
        
        public Vector2 ReadMoveInput(PlayerEnum playerEnum)
        {
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    return input.Player1.Move.ReadValue<Vector2>();
                case PlayerEnum.P2:
                    Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                    Vector3 mousePos = GlobalManager.ScreenToWorldPoint(mouseScreenPos);
                    if (Mouse.current.leftButton.isPressed)
                    {
                        Vector2 moveInput = (mousePos - transform.position).normalized;
                        Debug.DrawLine(transform.position, transform.position + (Vector3)moveInput, Color.red);
                        // Debug.Log(moveInput);
                        return moveInput;
                    }

                    return Vector2.zero;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool WasNosePerformThisFrame(PlayerEnum playerEnum)
        {
            switch (playerEnum)
            {
                case PlayerEnum.P1:
                    return input.Player1.Nose.WasPerformedThisFrame();
                case PlayerEnum.P2:
                    return Mouse.current.rightButton.wasPressedThisFrame;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}