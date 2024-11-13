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
        private Vector3 _currentPosition;
        private Vector2 _currentDirection;
        private Quaternion _currentRotation;

        private bool _isAccelerationState;

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
            _isAccelerationState = direction.y != 0f;
        }

        private void Accelerate(float delta)
        {
            _currentSpeed += delta;
            if (_currentSpeed > _config.MaxSpeed)
                _currentSpeed = _config.MaxSpeed;
        }

        private void Decelerate(float delta)
        {
            _currentSpeed -= delta;
            if (_currentSpeed < 0f)
                _currentSpeed = 0f;
        }

        void IUpdate.UpdateController(float deltaTime)
        {
            if (_isAccelerationState)
                Accelerate(deltaTime * _config.Acceleration);
            else
                Decelerate(deltaTime * _config.Deceleration);
            Quaternion deltaRotation = Quaternion.Euler(0f, 0f, -_inputDirection.x * deltaTime * _config.RotationSpeed);
            _currentRotation *= deltaRotation;

            Vector2 desiredDirection = _currentRotation * Vector2.up;
            _currentDirection = Vector2.Lerp(_currentDirection, desiredDirection, _config.AngularSpeed * deltaTime);
            _currentPosition += (Vector3) _currentDirection * (_currentSpeed * deltaTime);
            
            _view.position = _currentPosition;
            _view.rotation = _currentRotation;
        }
    }
}