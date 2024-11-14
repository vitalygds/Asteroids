using System;
using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class AsteroidConstructor : IUnitConstructor
    {
        private readonly IPoolService _poolService;
        private readonly ITickController _tickController;
        private readonly IUnitManager _manager;
        private readonly IRandomizer _randomizer;
        private readonly IUnitSpawner _spawner;
        private readonly IUnitDamageService _damageService;

        public UnitType Type => UnitType.Asteroid;

        public AsteroidConstructor(IPoolService poolService, ITickController tickController, IUnitManager manager, IRandomizer randomizer,
            IUnitSpawner spawner, IUnitDamageService damageService)
        {
            _poolService = poolService;
            _tickController = tickController;
            _manager = manager;
            _randomizer = randomizer;
            _spawner = spawner;
            _damageService = damageService;
        }

        public IUnit CreateUnit(UnitCreationArgs args, UnitConfig config, Vector3 position, Quaternion rotation)
        {
            if (config is AsteroidConfig ufoConfig)
            {
                return CreateUnit(args, ufoConfig, position, rotation);
            }

            throw new Exception("Invalid config type");
        }

        private IUnit CreateUnit(UnitCreationArgs args, AsteroidConfig config, Vector3 position, Quaternion rotation)
        {
            uint id = _manager.GetNextId();
            UnitView view = _poolService.Instantiate<UnitView>(config.Prefab, position, rotation);
            view.Initialize(id, args.OwnerLayer);
            PooledUnit unit = new PooledUnit(args.Id, id, _poolService, view);
            unit.AddComponent(new AsteroidMoveComponent(config, view.transform, _tickController));
            unit.AddComponent(new UnitObstacleComponent(unit, view, _damageService, args.TargetLayerMask));
            IDestroyComponent destroyComponent = new DestroyComponent(unit);
            unit.AddComponent(destroyComponent);
            if (config.SubAsteroids.Count > 0)
            {
                unit.AddComponent(new AsteroidChildComponent(args, config, view.transform, _spawner, _randomizer, destroyComponent));
            }

            _manager.AddUnit(unit);
            return unit;
        }
    }
}