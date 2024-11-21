using System;
using Infrastructure;
using UnityEngine;
using Weapon;

namespace Unit
{
    internal sealed class ShipConstructor : IUnitConstructor
    {
        private readonly IPoolService _poolService;
        private readonly ITickController _tickController;
        private readonly IUnitManager _manager;
        private readonly IWeaponService _weaponService;

        public UnitType Type => UnitType.Ship;

        public ShipConstructor(IPoolService poolService, ITickController tickController, IUnitManager manager, IWeaponService weaponService)
        {
            _poolService = poolService;
            _tickController = tickController;
            _manager = manager;
            _weaponService = weaponService;
        }

        public IUnit CreateUnit(UnitCreationArgs args, UnitConfig config, Vector3 position, Quaternion rotation)
        {
            if (config is ShipConfig ufoConfig)
            {
                return CreateUnit(args, ufoConfig, position, rotation);
            }

            throw new Exception("Invalid config type");
        }

        private IUnit CreateUnit(UnitCreationArgs args, ShipConfig config, Vector3 position, Quaternion rotation)
        {
            uint id = _manager.GetNextId();
            UnitView view = _poolService.Instantiate<UnitView>(config.Prefab, position, rotation);
            view.Initialize(id, args.OwnerLayer);

            PooledUnit unit = new PooledUnit(args.Id, id, _poolService, view);
            ShipAttackComponent attackComponent = new ShipAttackComponent(unit, view.transform);
            unit.AddComponent(attackComponent);
            WeaponCreationArgs weaponArgs = new WeaponCreationArgs(attackComponent, attackComponent, args.TargetLayerMask);
            for (int i = 0; i < config.Weapons.Count; i++)
            {
                string weaponId = config.Weapons[i];
                IWeapon weapon = _weaponService.CreateWeapon(weaponId, weaponArgs);
                attackComponent.AddWeapon(weapon);
            }

            unit.AddComponent(new ShipMoveComponent(config, view.transform, _tickController));
            unit.AddComponent(new DestroyComponent(unit));

            _manager.AddUnit(unit);
            return unit;
        }
    }
}