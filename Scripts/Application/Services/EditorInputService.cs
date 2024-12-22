using System;
using Assets.Scripts.Domain;
using Assets.Scripts.Domain.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Application.Services
{
    public class EditorInputService : IInputService
    {
        private readonly SelectMaterial _selectMaterial;
        private readonly Camera _camera;

        public event Action<InputAction.CallbackContext> OnSelect;
        public event Action OnDeselect;
        public event Action<Vector2> OnMouseMove;

        public EditorInputService(Camera camera)
        {
            _camera = camera;
            _selectMaterial = new SelectMaterial();
        }

        public void Initialize()
        {
            _selectMaterial.MaterialSelection.Select.performed += ctx => OnSelect?.Invoke(ctx);
            _selectMaterial.MaterialSelection.Select.started += ctx => OnSelect?.Invoke(ctx);
            _selectMaterial.MaterialSelection.Select.canceled += ctx => OnSelect?.Invoke(ctx);
            _selectMaterial.MaterialSelection.Deselect.performed += ctx => OnDeselect?.Invoke();
            _selectMaterial.MaterialSelection.WatchCursor.performed += ctx =>
                OnMouseMove?.Invoke(ctx.ReadValue<Vector2>());
        }

        public Vector2 GetMousePosition() =>
            _camera.ScreenToWorldPoint(
                _selectMaterial.MaterialSelection.WatchCursor.ReadValue<Vector2>()
            );

        public void Enable() => _selectMaterial.Enable();

        public void Disable() => _selectMaterial.Disable();
    }
}
