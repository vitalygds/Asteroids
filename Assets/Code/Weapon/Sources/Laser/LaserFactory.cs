using UnityEngine;

namespace Weapon
{
    internal sealed class LaserFactory : WeaponFactory<LaserConfig>
    {
        private readonly IWeaponUpdateManager _updateManager;
        private readonly IWeaponDamageMediator _damageMediator;
        private readonly RaycastHit2D[] _buffer;
        public override WeaponType Type => WeaponType.Laser;

        public LaserFactory(IWeaponUpdateManager updateManager, IWeaponDamageMediator damageMediator)
        {
            _updateManager = updateManager;
            _damageMediator = damageMediator;
            _buffer = new RaycastHit2D[64];
        }

        protected override IWeapon CreateWeapon(LaserConfig config, WeaponCreationArgs args)
        {
            LaserModel model = new LaserModel(config, args.User, args.TargetProvider, args.TargetLayerMask, _buffer,_damageMediator);
            SingleWeapon weapon = new SingleWeapon(model, config);
            _updateManager.RegisterUnit(weapon);
            _updateManager.RegisterUnit(model);
            return weapon;
        }
    }
}