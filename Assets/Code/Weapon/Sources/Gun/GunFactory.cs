using Infrastructure;

namespace Weapon
{
    internal sealed class GunFactory : WeaponFactory<GunConfig>
    {
        private readonly IWeaponUpdateManager _updateManager;
        private readonly IPoolService _poolService;
        private readonly IWeaponDamageMediator _damageMediator;

        public override WeaponType Type => WeaponType.Gun;

        public GunFactory(IWeaponUpdateManager updateManager, IPoolService poolService, IWeaponDamageMediator damageMediator)
        {
            _updateManager = updateManager;
            _poolService = poolService;
            _damageMediator = damageMediator;
        }

        protected override IWeapon CreateWeapon(GunConfig config, WeaponCreationArgs args)
        {
            GunModel model = new GunModel(config, args.User, args.TargetProvider, args.TargetLayerMask, _poolService, _damageMediator);
            SingleWeapon weapon = new SingleWeapon(model, config);
            _updateManager.RegisterUnit(model);
            _updateManager.RegisterUnit(weapon);
            return weapon;
        }
    }
}