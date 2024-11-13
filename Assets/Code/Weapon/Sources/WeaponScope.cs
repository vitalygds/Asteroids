using Infrastructure;

namespace Weapon
{
    public sealed class WeaponScope
    {
        public static void Build(IServiceLocator locator)
        {
            WeaponConfigLoader loader = new WeaponConfigLoader();
            WeaponUpdateManager updateManager = new WeaponUpdateManager(locator.Resolve<ITickController>());
            locator.Register<IWeaponUpdateManager>(updateManager);
            WeaponCompositeFactory compositeFactory =
                new WeaponCompositeFactory(ScopeFactories(locator, updateManager, locator.Resolve<IWeaponDamageMediator>()));
            WeaponService service = new WeaponService(updateManager, loader, compositeFactory);
            locator.Register<IWeaponService>(service);
        }

        private static IWeaponFactory[] ScopeFactories(IServiceLocator locator, IWeaponUpdateManager updateManager,
            IWeaponDamageMediator damageMediator)
        {
            return new IWeaponFactory[]
            {
                new GunFactory(updateManager, locator.Resolve<IPoolService>(), damageMediator), new LaserFactory(updateManager, damageMediator)
            };
        }
    }
}