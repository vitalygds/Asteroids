using System;
using System.Collections.Generic;
using Infrastructure;

namespace Weapon
{
    internal sealed class WeaponUpdateManager : IWeaponUpdateManager, IUpdate, IDisposable
    {
        private readonly IDisposable _updateSub;
        private readonly List<IUpdatableUnit> _controllers;

        public WeaponUpdateManager(ITickController tickController)
        {
            _controllers = new List<IUpdatableUnit>();
            _updateSub = tickController.AddController(this);
        }

        public void Dispose()
        {
            _updateSub?.Dispose();
            Clear();
        }

        public void RegisterUnit(IUpdatableUnit unit)
        {
            unit.OnDestroy += UnregisterController;
            _controllers.Add(unit);
        }

        public void Clear()
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                IUpdatableUnit controller = _controllers[i];
                controller.OnDestroy -= UnregisterController;
                controller.Destroy();
            }

            _controllers.Clear();
        }

        private void UnregisterController(IUpdatableUnit controller)
        {
            controller.OnDestroy -= UnregisterController;
            _controllers.Remove(controller);
        }

        void IUpdate.UpdateController(float deltaTime)
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                _controllers[i].Update(deltaTime);
            }
        }
    }
}