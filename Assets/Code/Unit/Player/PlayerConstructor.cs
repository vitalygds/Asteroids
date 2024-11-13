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
        
        public void CreatePlayer(string unitId, Vector3 position, Quaternion rotation)
        {
            PlayerConfig config = _configLoader.Load<PlayerConfig>(unitId);
            uint id = _manager.GetNextId();
            Transform view = _poolService.Instantiate<Transform>(config.Prefab, position, rotation);
            Player player = new Player(id, _poolService, view);
            
            PlayerMoveComponent moveComponent = new PlayerMoveComponent(config, view, _tickController);
            player.AddComponent(moveComponent);
            
            PlayerInputComponent inputComponent = new PlayerInputComponent(moveComponent, _inputService);
            player.AddComponent(inputComponent);
        }
    }
}