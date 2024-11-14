using System;
using Infrastructure;
using UnityEngine;
using Weapon;

namespace Unit
{
    internal sealed class UfoTargetComponent : ITargetComponent, IDestroyable, IUpdate, IAttackComponent, IWeaponUser, IWeaponTargetProvider
    {
        private readonly Unit _owner;
        private readonly Transform _view;
        private readonly IMoveComponent _moveComponent;
        private readonly IDisposable _updateSub;
        private bool _hasTarget;
        private Transform _target;
        private IWeapon _currentWeapon;

        public uint Id => _owner.Id;

        public Vector3 StartPosition => _view.position;
        public Vector3 EndPosition => _hasTarget ? _target.position : _view.position + _view.up;

        public UfoTargetComponent(Unit owner, Transform view, IMoveComponent moveComponent, ITickController tickController)
        {
            _owner = owner;
            _view = view;
            _moveComponent = moveComponent;
            _updateSub = tickController.AddController(this);
        }

        public void Destroy()
        {
            _updateSub.Dispose();
            _currentWeapon?.Destroy();
        }

        public void SetWeapon(IWeapon weapon)
        {
            _currentWeapon?.Destroy();
            _currentWeapon = weapon;
        }

        public void SetTarget(Transform target)
        {
            _hasTarget = target;
            _target = target;
            ActivateWeapon(true);
        }

        public void UnsetTarget()
        {
            _hasTarget = false;
            _target = null;
            _moveComponent.SetDirection(Vector2.zero);
            ActivateWeapon(false);
        }

        private void ActivateWeapon(bool isActive) => _currentWeapon?.SetActive(isActive);
        
        void IUpdate.UpdateController(float deltaTime)
        {
            if (_hasTarget)
            {
                _moveComponent.SetDirection((_target.position - _view.position).normalized);
            }
        }

        void IAttackComponent.SetActive(int value, bool isActive) => ActivateWeapon(isActive);
    }
}