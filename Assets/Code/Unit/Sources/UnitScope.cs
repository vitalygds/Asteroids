using Infrastructure;
using Weapon;

namespace Unit
{
    public sealed class UnitScope
    {
        public static void Build(IServiceLocator locator)
        {
            UnitService idService = new UnitService();
            locator.Register<IUnitManager>(idService);
            locator.Register<IUnitIdService>(idService);
            WeaponDamageMediator damageMediator = new WeaponDamageMediator(idService);
            locator.Register<IWeaponDamageMediator>(damageMediator);
        }

        public static void Complete(IServiceLocator locator)
        {
            UnitConfigLoader loader = new UnitConfigLoader();
            locator.Register<IUnitConfigLoader>(loader);
            UnitSpawner spawner = new UnitSpawner(loader);
            locator.Register<IUnitSpawner>(spawner);
            var constructors = ScopeConstructors(locator);
            spawner.Initialize(constructors);
        }

        private static IUnitConstructor[] ScopeConstructors(IServiceLocator locator)
        {
            ITickController tickController = locator.Resolve<ITickController>();
            IUnitManager unitManager = locator.Resolve<IUnitManager>();
            IPoolService poolService = locator.Resolve<IPoolService>();
            IWeaponService weaponService = locator.Resolve<IWeaponService>();
            ShipConstructor shipConstructor = new ShipConstructor(poolService, tickController, unitManager, weaponService);
            AsteroidConstructor asteroidConstructor = new AsteroidConstructor(poolService,
                tickController, unitManager, locator.Resolve<IRandomizer>(), locator.Resolve<IUnitSpawner>());
            UfoConstructor ufoConstructor = new UfoConstructor(poolService, tickController, unitManager, weaponService);
            return new IUnitConstructor[] { shipConstructor, asteroidConstructor, ufoConstructor };
        }
    }
}