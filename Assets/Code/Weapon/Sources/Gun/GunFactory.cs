using Infrastructure;

namespace Weapon
{
    internal sealed class GunFactory : WeaponFactory<GunConfig>
    {
        private readonly IWeaponUpdateManager _updateManager;
        private readonly IPoolService _poolService;

        public override WeaponType Type => WeaponType.Gun;

        public GunFactory(IWeaponUpdateManager updateManager, IPoolService poolService)
        {
            _updateManager = updateManager;
            _poolService = poolService;
        }

        protected override IWeapon CreateWeapon(GunConfig config, WeaponCreationArgs args)
        {
            GunModel model = new GunModel(config, args.User, args.TargetProvider, _poolService);
            SingleWeapon weapon = new SingleWeapon(model, config);
            _updateManager.RegisterUnit(model);
            _updateManager.RegisterUnit(weapon);
            return weapon;
        }
    }
}