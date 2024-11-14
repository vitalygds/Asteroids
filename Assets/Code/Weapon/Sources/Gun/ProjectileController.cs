using System;
using UnityEngine;

namespace Weapon
{
    internal sealed class ProjectileController
    {
        public event Action<ProjectileController> OnDestroy;
        private readonly IWeaponUser _owner;
        private readonly TriggerView _view;
        private readonly float _speed;
        private readonly IWeaponDamageMediator _damageMediator;
        private readonly Vector3 _direction;
        private float _lifeTime;

        public TriggerView View => _view;

        public ProjectileController(IWeaponUser owner, TriggerView view, float speed, float lifeTime, Vector3 direction,
            IWeaponDamageMediator damageMediator)
        {
            _owner = owner;
            _view = view;
            _speed = speed;
            _lifeTime = lifeTime;
            _damageMediator = damageMediator;
            _direction = direction.normalized;
            _view.OnTriggered += CheckTarget;
        }

        public void Destroy()
        {
            _view.OnTriggered -= CheckTarget;
        }

        public void Update(float deltaTime)
        {
            _view.transform.position += _direction * (_speed * deltaTime);
            _lifeTime -= deltaTime;
            if (_lifeTime <= 0f)
            {
                DestroyProjectile();
            }
        }

        private void DestroyProjectile()
        {
            _lifeTime = float.PositiveInfinity;
            OnDestroy?.Invoke(this);
        }

        private void CheckTarget(Collider2D other)
        {
            if (other.transform.TryGetComponent(out IDamageableView damageable))
            {
                if (_damageMediator.ApplyDamage(damageable, _owner))
                {
                    DestroyProjectile();
                }
            }
        }
    }
}