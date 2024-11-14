using System;
using System.Collections.Generic;
using Infrastructure;
using UI;
using Unit;
using UnityEngine;
using Weapon;

namespace Core
{
    internal sealed class PlayerScoreController : IDisposable, IEachSecondUpdate, IPlayerRuntimeInfoProvider
    {
        private readonly ITickController _tickController;
        private readonly IUnitDamageService _damageService;
        private readonly IChargeableModelMap _chargeableModelMap;
        private readonly IFieldViewProvider _fieldViewProvider;
        private readonly Dictionary<string, int> _scoreMap;
        private bool _started;
        private ScoreConfig _scoreConfig;
        private IDisposable _updateSub;
        private int _scorePerSecond;
        private int _currentScore;
        private uint _playerId;
        private Transform _playerTransform;
        private IMoveComponent _playerMoveComponent;
        private IChargeableWeaponModel _chargeableModel;
        private bool _hasChargeableWeapon;

        public int Score => _currentScore;
        public Vector2 Position => _playerTransform.position;
        public Quaternion Rotation => _playerTransform.rotation;
        public float Speed => _playerMoveComponent.Speed;
        public int WeaponCharges => _hasChargeableWeapon ? _chargeableModel.Charges : 0;
        public int WeaponMaxCharges => _hasChargeableWeapon ? _chargeableModel.MaxCharges : 0;
        public float ChargeTime => _hasChargeableWeapon ? _chargeableModel.Timer : 0f;

        public PlayerScoreController(ITickController tickController, IUnitDamageService damageService, IChargeableModelMap chargeableModelMap)
        {
            _tickController = tickController;
            _damageService = damageService;
            _chargeableModelMap = chargeableModelMap;
            _scoreMap = new Dictionary<string, int>();
        }

        public void Dispose()
        {
            Stop();
        }

        public void Start(IUnit player, ScoreConfig scoreConfig)
        {
            if (_started)
                return;
            _started = true;
            _playerId = player.Id;
            _playerTransform = player.Transform;
            _playerMoveComponent = player.GetComponent<IMoveComponent>();
            _currentScore = 0;
            _scorePerSecond = scoreConfig.PerSecondValue;
            for (int i = 0; i < scoreConfig.UnitPoints.Count; i++)
            {
                UnitPointsConfig kvp = scoreConfig.UnitPoints[i];
                _scoreMap[kvp.UnitId] = kvp.Value;
            }

            _hasChargeableWeapon = _chargeableModelMap.TryGetChargeableWeapon(_playerId, out _chargeableModel);
            _updateSub = _tickController.AddController(this);
            _damageService.OnUnitDamaged += TryAddScore;
        }

        public void Stop()
        {
            if (!_started)
                return;
            _started = false;
            _hasChargeableWeapon = false;
            _updateSub.Dispose();
            _damageService.OnUnitDamaged -= TryAddScore;
        }

        private void TryAddScore(uint from, IUnit to)
        {
            _chargeableModelMap.Remove(to.Id);
            if (from == _playerId)
            {
                if (_scoreMap.TryGetValue(to.Name, out int score))
                {
                    _currentScore += score;
                }
            }
        }

        void IEachSecondUpdate.UpdateController() => _currentScore += _scorePerSecond;
    }
}