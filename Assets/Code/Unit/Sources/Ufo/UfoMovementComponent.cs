using System;
using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class UfoMovementComponent : IMoveComponent, IDestroyable, IUpdate
    {
        private readonly UfoConfig _config;
        private readonly Transform _view;
        private readonly IDisposable _updateSub;
        
        private Vector3 _currentPosition;
        private Vector2 _currentDirection;
        private float _currentSpeed;
        
        private Vector2 _targetDirection;
        private bool _accelerate;
        private bool _dragRotation;
        private float _dragRotationFactor;
        
        public float Speed => _currentSpeed;

        public UfoMovementComponent(UfoConfig config, Transform view, ITickController tickController)
        {
            _config = config;
            _view = view;
            _currentPosition = view.position;
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
            _targetDirection = direction;
            _accelerate = direction.y != 0f;
        }

        public void AddSpeed(float value)
        {
            _currentSpeed += value;
        }

        private void Accelerate(float deltaTime)
        {
            _currentSpeed += deltaTime * _config.Acceleration;
            if (_currentSpeed > _config.MaxSpeed)
                _currentSpeed = _config.MaxSpeed;
        }

        private void Decelerate(float deltaTime)
        {
            _currentSpeed -= deltaTime * _config.Deceleration;
            if (_currentSpeed < 0f)
                _currentSpeed = 0f;
        }

        void IUpdate.UpdateController(float deltaTime)
        {
            if (_accelerate)
                Accelerate(deltaTime);
            else
                Decelerate(deltaTime);
            _currentDirection = Vector2.Lerp(_currentDirection, _targetDirection, _config.AngularSpeed * deltaTime);
            _currentPosition += (Vector3) _currentDirection * (_currentSpeed * deltaTime);
            _view.position = _currentPosition;
        }
    }
}