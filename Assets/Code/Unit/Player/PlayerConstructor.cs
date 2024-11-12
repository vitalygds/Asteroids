using System;
using Infrastructure;
using Input;
using UnityEngine;

namespace Unit
{
    internal sealed class PlayerConstructor
    {
        private readonly IUnitConfigLoader _configLoader;
        private readonly IPoolService _poolService;
        private readonly ITickController _tickController;
        private readonly IInputService _inputService;
        private readonly IUnitManager _manager;

        public PlayerConstructor(IUnitConfigLoader configLoader, IPoolService poolService, ITickController tickController, IInputService inputService,
            IUnitManager manager)
        {
            _poolService = poolService;
            _tickController = tickController;
            _inputService = inputService;
            _manager = manager;
            _configLoader = configLoader;
        }


        public void CreatePlayer(string unitId)
        {
            PlayerConfig config = _configLoader.Load<PlayerConfig>(unitId);
            uint id = _manager.GetNextId();
            Transform view = _poolService.Instantiate<Transform>(config.Prefab, Vector3.zero, Quaternion.identity);
            Player player = new Player(id, _poolService, view);
            
            PlayerMoveComponent moveComponent = new PlayerMoveComponent(config, view, _tickController);
            player.AddComponent(moveComponent);
            
            PlayerInputComponent inputComponent = new PlayerInputComponent(moveComponent, _inputService);
            player.AddComponent(inputComponent);
        }
    }

    internal sealed class Player : Unit
    {
        private readonly IPoolService _poolService;
        private readonly Transform _view;

        public Player(uint id, IPoolService poolService, Transform view) : base(id)
        {
            _poolService = poolService;
            _view = view;
        }


        protected override void DestroyCompletely()
        {
            _poolService.Destroy(_view.gameObject);
        }
    }


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