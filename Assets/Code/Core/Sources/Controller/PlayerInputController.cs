using System;
using Input;
using Unit;
using UnityEngine;

namespace Core
{
    internal sealed class PlayerInputController : IPlayerInputListener, IDisposable
    {
        private readonly IInputService _inputService;
        private IMoveComponent _moveComponent;
        private IAttackComponent _attackComponent;
        private IDisposable _inputSub;
        private IUnit _currentPlayer;

        public PlayerInputController(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Dispose()
        {
            _inputSub?.Dispose();
        }

        public void InitializePlayer(IUnit player)
        {
            if (_currentPlayer != null)
                return;

            _currentPlayer = player;
            _attackComponent = player.GetComponent<IAttackComponent>();
            _moveComponent = player.GetComponent<IMoveComponent>();
            _inputSub = _inputService.AddListener(this);
        }
        
        public void Stop()
        {
            _currentPlayer = null;
            _inputSub?.Dispose();
            _inputSub = null;
        }
        
        public void Move(Vector2 value)
        {
            if (value.y < 0f)
                value.y = 0f;
            _moveComponent.SetDirection(value);
        }

        public void OnFire(int value, bool isPressed)
        {
            _attackComponent.SetActive(value, isPressed);
        }
    }
}