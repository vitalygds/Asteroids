using System;
using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class ShipMoveComponent : IMoveComponent, IDestroyable, IUpdate
    {
        private readonly ShipConfig _config;
        private readonly Transform _view;
        private readonly IDisposable _updateSub;

        private float _currentSpeed;
        private float _currentRotationSpeed;
        private Vector3 _currentPosition;
        private Quaternion _currentRotation;
        private Vector2 _currentDirection;

        private Vector2 _targetDirectionLocal;
        private bool _accelerate;
        private bool _dragRotation;
        private float _dragRotationFactor;
        
        public float Speed => _currentSpeed;

        public ShipMoveComponent(ShipConfig config, Transform view, ITickController tickController)
        {
            _config = config;
            _view = view;
            _currentPosition = view.position;
            _currentRotation = view.rotation;
            _updateSub = tickController.AddController(this);
        }

        public void Destroy()
        {
            _updateSub.Dispose();
        }
        
        public void SetPosition(Vector3 position)
        {
            _currentPosition = position;
        }

        public void SetDirection(Vector2 direction)
        {
            _targetDirectionLocal = direction;
            _accelerate = direction.y != 0f;
            if (Mathf.Abs(_currentRotationSpeed) <= Mathf.Abs(direction.x))
                _currentRotationSpeed = direction.x;
        }

        public void AddSpeed(float value)
        {
            _currentSpeed += value;
        }

        private void Accelerate(float deltaTime)
        {
            _currentSpeed += deltaTime * _config.Deceleration;
            if (_currentSpeed > _config.MaxSpeed)
                _currentSpeed = _config.MaxSpeed;
        }

        private void Decelerate(float deltaTime)
        {
            _currentSpeed -= deltaTime * _config.Acceleration;
            if (_currentSpeed < 0f)
                _currentSpeed = 0f;
        }

        void IUpdate.UpdateController(float deltaTime)
        {
            if (_accelerate)
                Accelerate(deltaTime);
            else
                Decelerate(deltaTime);
            Quaternion deltaRotation = Quaternion.Euler(0f, 0f, -_currentRotationSpeed * deltaTime * _config.RotationSpeed);
            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, _targetDirectionLocal.x, _config.AngularDrag * deltaTime);
            _currentRotation *= deltaRotation;
            Vector2 desiredDirection = _currentRotation * Vector2.up;
            _currentDirection = Vector2.Lerp(_currentDirection, desiredDirection, _config.AngularSpeed * deltaTime);
            _currentPosition += (Vector3) _currentDirection * (_currentSpeed * deltaTime);
            _view.SetPositionAndRotation(_currentPosition, _currentRotation);
        }
    }
}