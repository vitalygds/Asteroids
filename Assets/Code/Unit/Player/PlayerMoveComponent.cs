using System;
using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class PlayerMoveComponent : IUpdate, IUnitComponent, IDestroyableComponent
    {
        private readonly PlayerConfig _config;
        private readonly Transform _view;
        private readonly IDisposable _updateSub;
        private Vector2 _inputDirection;
        private float _currentSpeed;
        private float _currentRotationSpeed;
        private Vector3 _currentPosition;
        private Vector2 _currentDirection;
        private Quaternion _currentRotation;

        private bool _accelerate;
        private bool _dragRotation;
        private float _dragRotationFactor;

        public PlayerMoveComponent(PlayerConfig config, Transform view, ITickController tickController)
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

        public void SetDirection(Vector2 direction)
        {
            _inputDirection = direction;
            _accelerate = direction.y != 0f;
            if (Mathf.Abs(_currentRotationSpeed) < Mathf.Abs(direction.x))
            {
                _currentRotationSpeed += direction.x;
            }
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
            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, _inputDirection.x, _config.AngularDrag * deltaTime);
            _currentRotation *= deltaRotation;
            Vector2 desiredDirection = _currentRotation * Vector2.up;
            _currentDirection = Vector2.Lerp(_currentDirection, desiredDirection, _config.AngularSpeed * deltaTime);
            _currentPosition += (Vector3) _currentDirection * (_currentSpeed * deltaTime);
            _view.SetPositionAndRotation(_currentPosition, _currentRotation);
        }
    }
}