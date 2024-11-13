using Infrastructure;
using Input;
using UnityEngine;
using Weapon;

namespace Unit
{
    internal sealed class PlayerConstructor
    {
        private readonly IUnitConfigLoader _configLoader;
        private readonly IPoolService _poolService;
        private readonly ITickController _tickController;
        private readonly IInputService _inputService;
        private readonly IUnitManager _manager;
        private readonly IWeaponService _weaponService;

        public PlayerConstructor(IUnitConfigLoader configLoader, IPoolService poolService, ITickController tickController, IInputService inputService,
            IUnitManager manager, IWeaponService weaponService)
        {
            _poolService = poolService;
            _tickController = tickController;
            _inputService = inputService;
            _manager = manager;
            _weaponService = weaponService;
            _configLoader = configLoader;
        }

        public void CreatePlayer(string unitId, Vector3 position, Quaternion rotation)
        {
            PlayerConfig config = _configLoader.Load<PlayerConfig>(unitId);
            uint id = _manager.GetNextId();
            Transform view = _poolService.Instantiate<Transform>(config.Prefab, position, rotation);
            Player player = new Player(id, _poolService, view);

            PlayerAttackComponent attackComponent = new PlayerAttackComponent(player, view);
            player.AddComponent(attackComponent);
            WeaponCreationArgs weaponArgs = new WeaponCreationArgs(attackComponent, attackComponent);
            for (int i = 0; i < config.Weapons.Count; i++)
            {
                string weaponId = config.Weapons[i];
                IWeapon weapon = _weaponService.CreateWeapon(weaponId, weaponArgs);
                attackComponent.AddWeapon(weapon);
            }


            PlayerMoveComponent moveComponent = new PlayerMoveComponent(config, view, _tickController);
            player.AddComponent(moveComponent);

            PlayerInputComponent inputComponent = new PlayerInputComponent(moveComponent, attackComponent, _inputService);
            player.AddComponent(inputComponent);
        }
    }
}