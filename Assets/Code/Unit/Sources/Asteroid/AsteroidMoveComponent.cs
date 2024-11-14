using System;
using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class AsteroidMoveComponent : IMoveComponent, IDestroyable, IUpdate
    {
        private readonly AsteroidConfig _config;
        private readonly Transform _view;
        private readonly IDisposable _updateSub;
        private Vector3 _currentPosition;
        private Quaternion _currentRotation;
        private Vector2 _currentDirection;
        private Vector2 _additiveForce;
        private float _currentSpeed;

        public float Speed => _currentSpeed;

        public AsteroidMoveComponent(AsteroidConfig config, Transform view, ITickController tickController)
        {
            _config = config;
            _view = view;
            _currentPosition = view.position;
            _currentRotation = view.rotation;
            _currentSpeed = config.MinSpeed;
            _updateSub = tickController.AddController(this);
        }

        public void SetPosition(Vector3 position)
        {
            _currentPosition = position;
        }

        public void SetDirection(Vector2 direction)
        {
            _currentDirection = direction;
        }

        public void AddSpeed(float value)
        {
            _currentSpeed += value;
        }

        public void Destroy()
        {
            _updateSub.Dispose();
        }

        void IUpdate.UpdateController(float deltaTime)
        {
            Quaternion deltaRotation = Quaternion.Euler(0f, 0f, deltaTime * _config.RotationSpeed);
            _currentRotation *= deltaRotation;
            _currentPosition += (Vector3) ((_currentDirection * _currentSpeed + _additiveForce) * deltaTime);
            _currentSpeed = Mathf.Lerp(_currentSpeed, _config.MinSpeed, _config.Deceleration * deltaTime);
            _view.SetPositionAndRotation(_currentPosition, _currentRotation);
        }
    }
}