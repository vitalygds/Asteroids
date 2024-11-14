using System;
using Infrastructure;
using UnityEngine;
using Weapon;

namespace Unit
{
    internal sealed class UfoConstructor : IUnitConstructor
    {
        private readonly IPoolService _poolService;
        private readonly ITickController _tickController;
        private readonly IUnitManager _manager;
        private readonly IWeaponService _weaponService;
        private readonly IUnitDamageService _damageService;

        public UnitType Type => UnitType.Ufo;

        public UfoConstructor(IPoolService poolService, ITickController tickController, IUnitManager manager, IWeaponService weaponService,
            IUnitDamageService damageService)
        {
            _poolService = poolService;
            _tickController = tickController;
            _manager = manager;
            _weaponService = weaponService;
            _damageService = damageService;
        }

        public IUnit CreateUnit(UnitCreationArgs args, UnitConfig config, Vector3 position, Quaternion rotation)
        {
            if (config is UfoConfig ufoConfig)
            {
                return CreateUnit(args, ufoConfig, position, rotation);
            }

            throw new Exception("Invalid config type");
        }

        private IUnit CreateUnit(UnitCreationArgs args, UfoConfig config, Vector3 position, Quaternion rotation)
        {
            uint id = _manager.GetNextId();
            UnitView view = _poolService.Instantiate<UnitView>(config.Prefab, position, Quaternion.identity);
            view.Initialize(id, args.OwnerLayer);
            PooledUnit unit = new PooledUnit(args.Id, id, _poolService, view);

            UfoMovementComponent movementComponent = new UfoMovementComponent(config, view.transform, _tickController);
            unit.AddComponent(movementComponent);

            UfoTargetComponent targetComponent = new UfoTargetComponent(unit, view.transform, movementComponent, _tickController);
            unit.AddComponent(targetComponent);

            if (!string.IsNullOrEmpty(config.Weapon))
            {
                IWeapon weapon = _weaponService.CreateWeapon(config.Weapon,
                    new WeaponCreationArgs(targetComponent, targetComponent, args.TargetLayerMask));
                targetComponent.SetWeapon(weapon);
            }

            IDestroyComponent destroyComponent = new DestroyComponent(unit);
            unit.AddComponent(destroyComponent);
            _manager.AddUnit(unit);
            return unit;
        }
    }
}