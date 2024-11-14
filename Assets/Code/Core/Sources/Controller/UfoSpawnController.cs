using Infrastructure;
using Unit;
using UnityEngine;

namespace Core
{
    internal sealed class UfoSpawnController : UnitSpawnController
    {
        private Transform _target;

        public UfoSpawnController(IUnitSpawner unitSpawner, IUnitManager unitManager, ITickController tickController, IRandomizer randomizer,
            IFieldViewProvider provider) : base(unitSpawner, unitManager, tickController, randomizer, provider)
        {
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        protected override void InitializeUnit(IUnit unit)
        {
            base.InitializeUnit(unit);
            if (unit.TryGetComponent(out ITargetComponent targetComponent))
            {
                targetComponent.SetTarget(_target);
            }
        }
    }
}