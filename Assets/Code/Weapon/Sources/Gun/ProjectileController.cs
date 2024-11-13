using System;
using UnityEngine;

namespace Weapon
{
    internal sealed class ProjectileController
    {
        public event Action<ProjectileController> OnDestroy;
        private readonly TriggerView _view;
        private readonly float _speed;
        private readonly Vector3 _direction;
        private float _lifeTime;

        public TriggerView View => _view;

        public ProjectileController(TriggerView view, float speed, float lifeTime, Vector3 direction)
        {
            _view = view;
            _speed = speed;
            _lifeTime = lifeTime;
            _direction = direction.normalized;
            _view.OnTriggered += CheckTarget;
        }

        public void Destroy()
        {
            _view.OnTriggered -= CheckTarget;
            OnDestroy?.Invoke(this);
        }

        public void Update(float deltaTime)
        {
            _view.transform.position += _direction * (_speed * deltaTime);
            _lifeTime -= deltaTime;
            if (_lifeTime <= 0f)
            {
                _lifeTime = float.PositiveInfinity;
                Destroy();
            }
        }

        private void CheckTarget(Collider2D other)
        {
        }
    }
}