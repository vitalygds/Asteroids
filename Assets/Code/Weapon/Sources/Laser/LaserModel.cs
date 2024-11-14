using System;
using Infrastructure;
using UnityEngine;

namespace Weapon
{
    internal sealed class LaserModel : IWeaponModel, IUpdatableUnit, IChargeableWeaponModel
    {
        public event Action<IUpdatableUnit> OnDestroy;
        private readonly LaserConfig _config;
        private readonly IWeaponUser _owner;
        private readonly IWeaponTargetProvider _targetProvider;
        private readonly IWeaponDamageMediator _damageMediator;
        private readonly RaycastHit2D[] _buffer;
        private readonly ContactFilter2D _filter;
        public int Charges { get; private set; }
        public int MaxCharges => _config.MaxCharges;
        public float Timer { get; private set; }


        public LaserModel(LaserConfig config, IWeaponUser owner, IWeaponTargetProvider targetProvider, int targetMask,
            RaycastHit2D[] buffer, IWeaponDamageMediator damageMediator)
        {
            _config = config;
            _owner = owner;
            _targetProvider = targetProvider;
            _damageMediator = damageMediator;
            _buffer = buffer;
            Charges = config.StartCharges;
            Timer = config.ChargeTime;
            _filter = new ContactFilter2D {useLayerMask = true, layerMask = targetMask, useTriggers = true};
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }

        public bool Attack()
        {
            if (Charges > 0)
            {
                if (Charges == _config.MaxCharges)
                {
                    Timer = _config.ChargeTime;
                }

                Charges--;
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
            Timer -= deltaTime;
            if (Timer <= 0)
            {
                Charges++;
                if (Charges >= _config.MaxCharges)
                {
                    Charges = _config.MaxCharges;
                    Timer = float.PositiveInfinity;
                }
                else
                {
                    Timer = _config.ChargeTime;
                }
            }
        }
    }
}