using System;
using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class AsteroidChildComponent : IChildComponent, IDestroyable
    {
        public event Action<IUnit> OnChildCreated;
        private readonly UnitCreationArgs _ownerArgs;
        private readonly AsteroidConfig _config;
        private readonly Transform _view;
        private readonly IUnitSpawner _spawner;
        private readonly IRandomizer _randomizer;
        private readonly IDestroyComponent _destroyComponent;


        public AsteroidChildComponent(UnitCreationArgs ownerArgs, AsteroidConfig config, Transform view, IUnitSpawner spawner, IRandomizer randomizer,
            IDestroyComponent destroyComponent)
        {
            _ownerArgs = ownerArgs;
            _config = config;
            _view = view;
            _spawner = spawner;
            _randomizer = randomizer;
            _destroyComponent = destroyComponent;
            destroyComponent.OnDestroy += CreateChild;
        }

        public void Destroy()
        {
            _destroyComponent.OnDestroy -= CreateChild;
        }

        private void CreateChild()
        {
            int count = _randomizer.GetRandom(_config.SubsCountMin, _config.SubsCountMax + 1);
            Vector3 centralPoint = _view.position;
            for (int i = 0; i < count; i++)
            {
                string id = _config.SubAsteroids[_randomizer.GetRandom(0, _config.SubAsteroids.Count)];
                Vector2 direction = _randomizer.OnUnitCircle();
                IUnit child = _spawner.SpawnUnit(new UnitCreationArgs(id, _ownerArgs.OwnerLayer, _ownerArgs.TargetLayerMask),
                    centralPoint + (Vector3) direction, Quaternion.LookRotation(Vector3.forward, direction));
                if (child.TryGetComponent(out IMoveComponent moveComponent))
                {
                    moveComponent.SetDirection(direction);
                    moveComponent.AddSpeed(_config.SubsAdditiveForce);
                }

                OnChildCreated?.Invoke(child);
            }
        }
    }
}