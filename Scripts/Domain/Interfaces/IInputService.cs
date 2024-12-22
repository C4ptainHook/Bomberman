using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Domain.Interfaces
{
    public interface IInputService
    {
        Vector2 GetMousePosition();
        void Initialize();
        void Enable();
        void Disable();
        event Action<InputAction.CallbackContext> OnSelect;
        event Action OnDeselect;
        event Action<Vector2> OnMouseMove;
    }
}
