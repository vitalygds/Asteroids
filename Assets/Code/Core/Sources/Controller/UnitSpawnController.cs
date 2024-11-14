using System;
using System.Collections.Generic;
using Infrastructure;
using Unit;
using UnityEngine;

namespace Core
{
    internal class UnitSpawnController : IDisposable, IUpdate, IEachSecondUpdate
    {
        private readonly IUnitSpawner _unitSpawner;
        private readonly ITickController _tickController;
        private readonly IRandomizer _randomizer;
        private readonly IFieldViewProvider _provider;
        private readonly IUnitManager _unitManager;
        private readonly List<IUnit> _units;
        private UnitSpawnLevelConfig _config;
        private bool _started;
        private bool _canSpawn;
        private float _cooldown;
        private IDisposable _updateSub;
        private Vector2 _bottomLeftDeadZone;
        private Vector2 _topRightDeadZone;

        public UnitSpawnController(IUnitSpawner unitSpawner, IUnitManager unitManager, ITickController tickController, IRandomizer randomizer,
            IFieldViewProvider provider)
        {
            _unitSpawner = unitSpawner;
            _unitManager = unitManager;
            _tickController = tickController;
            _randomizer = randomizer;
            _provider = provider;
            _units = new List<IUnit>(128);
            provider.OnUpdate += UpdateBorders;
        }

        public void Dispose()
        {
            _updateSub?.Dispose();
            DestroyUnits();
        }

        public void Start(UnitSpawnLevelConfig config)
        {
            if (_started)
                return;
            _started = true;
            _config = config;
            UpdateBorders(_provider);
            _updateSub = _tickController.AddController(this);
        }

        public void Stop()
        {
            _updateSub?.Dispose();
            DestroyUnits();
        }

        protected virtual void InitializeUnit(IUnit unit)
        {
            unit.OnDestroy += RemoveUnit;
            if (unit.TryGetComponent(out IChildComponent childComponent))
            {
                childComponent.OnChildCreated += OnChildCreated;
            }

            _units.Add(unit);
            SetSpawnState();
        }

        private void UpdateBorders(IFieldViewProvider provider)
        {
            _bottomLeftDeadZone = new Vector2(provider.BottomLeft.x - _config.DeadZoneBound, provider.BottomLeft.y - _config.DeadZoneBound);
            _topRightDeadZone = new Vector2(provider.TopRight.x + _config.DeadZoneBound, provider.TopRight.y + _config.DeadZoneBound);
        }

        private void CreateRandomUnit()
        {
            string unitId = _config.Ids[_randomizer.GetRandom(0, _config.Ids.Count)];
            (Vector3, Quaternion, Vector2) spawnParams = RandomizeSpawnParams();
            IUnit unit = _unitSpawner.SpawnUnit(new UnitCreationArgs(unitId, LayerUtils.EnemyLayer, LayerUtils.PlayerMask), spawnParams.Item1,
                spawnParams.Item2);
            InitializeUnit(unit);
            if (unit.TryGetComponent(out IMoveComponent moveComponent))
            {
                moveComponent.SetDirection(spawnParams.Item3);
            }
        }

        private void OnChildCreated(IUnit unit)
        {
            InitializeUnit(unit);
        }

        private void RemoveUnit(IUnit unit)
        {
            UnsubscribeUnit(unit);
            _units.Remove(unit);
            SetSpawnState();
        }

        private void SetSpawnState() => _canSpawn = _units.Count < _config.MaxCount;

        private void UnsubscribeUnit(IUnit unit)
        {
            unit.OnDestroy -= RemoveUnit;
            if (unit.TryGetComponent(out IChildComponent childComponent))
            {
                childComponent.OnChildCreated -= OnChildCreated;
            }
        }

        private void DestroyUnits()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                IUnit unit = _units[i];
                UnsubscribeUnit(unit);
                _unitManager.DestroyUnit(unit.Id);
            }

            _units.Clear();
        }

        private (Vector3, Quaternion, Vector2) RandomizeSpawnParams()
        {
            Vector3 position;
            Vector2 direction;

            bool isVertical = _randomizer.GetRandomBool();
            if (isVertical)
            {
                float xPosFrom = _randomizer.GetRandom(_provider.BottomLeft.x, _provider.TopRight.x);
                float xPosTo = _randomizer.GetRandom(_provider.BottomLeft.x, _provider.TopRight.x);
                bool isFromTop = _randomizer.GetRandomBool();
                float yPos = isFromTop ? _topRightDeadZone.y : _bottomLeftDeadZone.y;
                position = new Vector3(xPosFrom, yPos, 0f);
                Vector3 to = new Vector3(xPosTo, isFromTop ? _bottomLeftDeadZone.y : _topRightDeadZone.y, 0f);
                direction = (to - position).normalized;
            }
            else
            {
                float yPosFrom = _randomizer.GetRandom(_provider.BottomLeft.y, _provider.TopRight.y);
                float yPosTo = _randomizer.GetRandom(_provider.BottomLeft.y, _provider.TopRight.y);
                bool isFromLeft = _randomizer.GetRandomBool();
                float xPos = isFromLeft ? _bottomLeftDeadZone.x : _topRightDeadZone.x;
                position = new Vector3(xPos, yPosFrom, 0f);
                Vector3 to = new Vector3(isFromLeft ? _topRightDeadZone.x : _bottomLeftDeadZone.x, yPosTo, 0f);
                direction = (to - position).normalized;
            }

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

            return (position, rotation, direction);
        }

        void IUpdate.UpdateController(float deltaTime)
        {
            _cooldown -= deltaTime;
            if (_canSpawn && _cooldown <= 0f)
            {
                CreateRandomUnit();
                _cooldown = _config.SpawnTick;
            }
        }

        void IEachSecondUpdate.UpdateController()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                IUnit unit = _units[i];
                Vector3 position = unit.Transform.position;
                if (position.x < _bottomLeftDeadZone.x || position.x > _topRightDeadZone.x || position.y < _bottomLeftDeadZone.y ||
                    position.y > _topRightDeadZone.y)
                {
                    (Vector3, Quaternion, Vector2) respawnParams = RandomizeSpawnParams();
                    if (unit.TryGetComponent(out IMoveComponent moveComponent))
                    {
                        moveComponent.SetPosition(respawnParams.Item1);
                        moveComponent.SetDirection(respawnParams.Item3);
                    }
                }
            }

            SetSpawnState();
            Debug.DrawLine(_bottomLeftDeadZone, _topRightDeadZone, Color.red, 1f);
        }
    }
}