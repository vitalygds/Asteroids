using System;
using Input;
using UnityEngine;

namespace Unit
{
    internal sealed class PlayerInputComponent : IPlayerInputListener, IUnitComponent, IDestroyableComponent
    {
        private readonly PlayerMoveComponent _moveComponent;
        private readonly IDisposable _inputSub;

        public PlayerInputComponent(PlayerMoveComponent moveComponent, IInputService inputService)
        {
            _moveComponent = moveComponent;
            _inputSub = inputService.AddListener(this);
        }

        public void Destroy()
        {
            _inputSub.Dispose();
        }

        public void Move(Vector2 value)
        {
            if (value.y < 0f)
                value.y = 0f;
            _moveComponent.SetDirection(value);
        }

        public void OnFire(int value, bool isPressed)
        {
           
        }
    }
}