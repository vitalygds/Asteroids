using VContainer;
using Weapon;

namespace Unit
{
    public sealed class UnitScope
    {
        public static void Build(IContainerBuilder builder)
        {
            builder.Register<IUnitManager, IUnitIdService, UnitService>(Lifetime.Singleton);
            builder.Register<IWeaponDamageMediator, IUnitDamageService, UnitDamageService>(Lifetime.Singleton);
            builder.Register<IUnitConfigLoader, UnitConfigLoader>(Lifetime.Singleton);
            builder.Register<IUnitSpawner, UnitSpawner>(Lifetime.Singleton).AsSelf();
            builder.Register<IUnitConstructor, ShipConstructor>(Lifetime.Singleton);
            builder.Register<IUnitConstructor, AsteroidConstructor>(Lifetime.Singleton);
            builder.Register<IUnitConstructor, UfoConstructor>(Lifetime.Singleton);
            builder.Register<UnitServiceResolver>(Lifetime.Singleton);
        }
    }
}