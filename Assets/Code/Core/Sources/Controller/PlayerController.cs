using System;
using Infrastructure;
using Unit;
using UnityEngine;

namespace Core
{
    internal sealed class PlayerController : IDisposable, IFixedUpdate
    {
        private readonly PlayerInputController _inputController;
        private readonly IUnitSpawner _spawner;
        private readonly IFieldViewProvider _fieldViewProvider;
        private readonly ITickController _tickController;
        private IDisposable _updateSub;
        private IMoveComponent _moveComponent;
        private IUnit _player;

        public PlayerController(PlayerInputController inputController, IUnitSpawner spawner, IFieldViewProvider fieldViewProvider,
            ITickController tickController)
        {
            _inputController = inputController;
            _spawner = spawner;
            _fieldViewProvider = fieldViewProvider;
            _tickController = tickController;
        }

        public void Dispose()
        {
            _inputController?.Dispose();
            _updateSub?.Dispose();
        }

        public IUnit Start(string playerName)
        {
            _player = _spawner.SpawnUnit(new UnitCreationArgs(playerName, LayerUtils.PlayerLayer, LayerUtils.EnemyMask), Vector3.zero, Quaternion.identity);
            _player.OnDestroy += UnsubscribePlayer;
            _moveComponent = _player.GetComponent<IMoveComponent>();
            _inputController.InitializePlayer(_player);
            _updateSub = _tickController.AddController(this);
            return _player;
        }

        private void UnsubscribePlayer(IUnit player)
        {
            _player.OnDestroy -= UnsubscribePlayer;
            _inputController.Stop();
            _updateSub?.Dispose();
        }

        void IFixedUpdate.UpdateController(float deltaTime)
        {
            Vector3 position = _player.Transform.position;
            if (position.x < _fieldViewProvider.BottomLeft.x)
            {
                position.x = _fieldViewProvider.TopRight.x;
            }
            else if (position.x > _fieldViewProvider.TopRight.x)
            {
                position.x = _fieldViewProvider.BottomLeft.x;
            }

            if (position.y < _fieldViewProvider.BottomLeft.y)
            {
                position.y = _fieldViewProvider.TopRight.y;
            }
            else if (position.y > _fieldViewProvider.TopRight.y)
            {
                position.y = _fieldViewProvider.BottomLeft.y;
            }

            _moveComponent.SetPosition(position);
        }
    }
}