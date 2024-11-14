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

        public UnitType Type => UnitType.Asteroid;

        public AsteroidConstructor(IPoolService poolService, ITickController tickController, IUnitManager manager, IRandomizer randomizer,
            IUnitSpawner spawner)
        {
            _poolService = poolService;
            _tickController = tickController;
            _manager = manager;
            _randomizer = randomizer;
            _spawner = spawner;
        }

        public IUnit CreateUnit(UnitConfig config, Vector3 position, Quaternion rotation)
        {
            if (config is AsteroidConfig ufoConfig)
            {
                return CreateUnit(ufoConfig, position, rotation);
            }

            throw new Exception("Invalid config type");
        }

        private IUnit CreateUnit(AsteroidConfig config, Vector3 position, Quaternion rotation)
        {
            uint id = _manager.GetNextId();
            UnitView view = _poolService.Instantiate<UnitView>(config.Prefab, position, rotation);
            view.Initialize(id, LayerUtils.EnemyLayer);
            PooledUnit unit = new PooledUnit(id, _poolService, view);
            AsteroidMoveComponent moveComponent = new AsteroidMoveComponent(config, view.transform, _tickController);
            unit.AddComponent(moveComponent);

            IDestroyComponent destroyComponent = new DestroyComponent(unit);
            unit.AddComponent(destroyComponent);
            if (config.SubAsteroids.Count > 0)
            {
                AsteroidChildComponent childComponent = new AsteroidChildComponent(config, view.transform, _spawner, _randomizer, destroyComponent);
                unit.AddComponent(childComponent);
            }

            _manager.AddUnit(unit);
            return unit;
        }
    }
}