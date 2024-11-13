using System;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Weapon
{
    internal sealed class GunModel : IWeaponModel, IUpdatableUnit
    {
        public event Action<IUpdatableUnit> OnDestroy;
        private readonly GunConfig _config;
        private readonly IWeaponUser _owner;
        private readonly IWeaponTargetProvider _targetProvider;
        private readonly IPoolService _poolService;
        private readonly IWeaponUpdateManager _updateManager;
        private readonly List<ProjectileController> _controllers;

        public GunModel(GunConfig config, IWeaponUser owner, IWeaponTargetProvider targetProvider, IPoolService poolService)
        {
            _config = config;
            _owner = owner;
            _targetProvider = targetProvider;
            _poolService = poolService;
            _controllers = WeaponCollections.GetList<ProjectileController>();
        }

        public void Attack()
        {
            Vector3 startPosition = _targetProvider.StartPosition;
            Vector3 endPosition = _targetProvider.EndPosition;
            TriggerView instance = _poolService.Instantiate<TriggerView>(_config.ProjectilePrefab, startPosition, Quaternion.identity);
            ProjectileController controller = new ProjectileController(instance, _config.ProjectileSpeed, _config.ProjectileLifetime,
                (endPosition - startPosition).normalized);
            controller.OnDestroy += RemoveController;
            _controllers.Add(controller);
        }

        private void RemoveController(ProjectileController controller)
        {
            ReleaseController(controller);
            _controllers.Remove(controller);
        }

        private void ReleaseController(ProjectileController controller)
        {
            controller.OnDestroy -= RemoveController;
            _poolService.Destroy(controller.View.gameObject);
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            for (int i = 0; i < _controllers.Count; i++)
            {
                ReleaseController(_controllers[i]);
            }

            _controllers.Clear();
            WeaponCollections.Release(_controllers);
        }

        void IUpdatableUnit.Update(float deltaTime)
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                _controllers[i].Update(deltaTime);
            }
        }
    }
}