using System;
using Infrastructure;
using UnityEngine;

namespace Weapon
{
    internal sealed class LaserModel : IWeaponModel, IUpdatableUnit
    {
        public event Action<IUpdatableUnit> OnDestroy;
        private readonly LaserConfig _config;
        private readonly IWeaponUser _owner;
        private readonly IWeaponTargetProvider _targetProvider;
        private readonly IWeaponDamageMediator _damageMediator;
        private readonly RaycastHit2D[] _buffer;
        private readonly ContactFilter2D _filter;
        private int _charges;
        private float _timer;


        public LaserModel(LaserConfig config, IWeaponUser owner, IWeaponTargetProvider targetProvider, int targetMask,
            RaycastHit2D[] buffer, IWeaponDamageMediator damageMediator)
        {
            _config = config;
            _owner = owner;
            _targetProvider = targetProvider;
            _damageMediator = damageMediator;
            _buffer = buffer;
            _charges = config.StartCharges;
            _timer = config.ChargeTime;
            _filter = new ContactFilter2D {useLayerMask = true, layerMask = targetMask, useTriggers = true};
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }

        public bool Attack()
        {
            if (_charges > 0)
            {
                if (_charges == _config.MaxCharges)
                {
                    _timer = _config.ChargeTime;
                }

                _charges--;
                Vector3 startPosition = _targetProvider.StartPosition;
                Vector3 direction = (_targetProvider.EndPosition - startPosition).normalized;
                int targetsCount = Physics2D.CircleCast(startPosition, _config.Width * 0.5f, direction, _filter, _buffer);
                DrawInGame.Box(startPosition + direction * (_config.Lenght * 0.5f), Quaternion.LookRotation(Vector3.forward, direction),
                    new Vector2(_config.Width, _config.Lenght), _config.Color, _config.VisualTime);

                for (int i = 0; i < targetsCount; i++)
                {
                    RaycastHit2D target = _buffer[i];
                    if (target.transform.TryGetComponent(out IDamageableView damageable))
                    {
                        _damageMediator.ApplyDamage(damageable, _owner);
                    }
                }

                return true;
            }

            return false;
        }

        void IUpdatableUnit.Update(float deltaTime)
        {
            _timer -= deltaTime;
            if (_timer <= 0)
            {
                _charges++;
                if (_charges >= _config.MaxCharges)
                {
                    _charges = _config.MaxCharges;
                    _timer = float.PositiveInfinity;
                }
                else
                {
                    _timer = _config.ChargeTime;
                }
            }
        }
    }
}