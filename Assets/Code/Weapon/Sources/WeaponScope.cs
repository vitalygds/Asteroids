using Infrastructure;

namespace Weapon
{
    public sealed class WeaponScope
    {
        public WeaponScope(IServiceLocator locator)
        {
            WeaponConfigLoader loader = new WeaponConfigLoader();
            WeaponUpdateManager updateManager = new WeaponUpdateManager(locator.Resolve<ITickController>());
            locator.Register<IWeaponUpdateManager>(updateManager);
            WeaponCompositeFactory compositeFactory = new WeaponCompositeFactory(ScopeFactories(locator, updateManager));
            WeaponService service = new WeaponService(updateManager, loader, compositeFactory);
            locator.Register<IWeaponService>(service);
        }

        private IWeaponFactory[] ScopeFactories(IServiceLocator locator, IWeaponUpdateManager updateManager)
        {
            return new[] {new GunFactory(updateManager, locator.Resolve<IPoolService>())};
        }
    }
}