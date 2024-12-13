using VContainer;

namespace Weapon
{
    public sealed class WeaponScope
    {
        public static void Build(IContainerBuilder builder)
        {
            builder.Register<IWeaponConfigLoader, WeaponConfigLoader>(Lifetime.Singleton);
            builder.Register<IWeaponUpdateManager, WeaponUpdateManager>(Lifetime.Singleton).AsSelf();
            builder.Register<IChargeableModelMap, ChargeableModelMap>(Lifetime.Singleton);
            builder.Register<IWeaponCompositeFactory, WeaponCompositeFactory>(Lifetime.Singleton);
            builder.Register<IWeaponService, WeaponService>(Lifetime.Singleton);
            
            builder.Register<IWeaponFactory, GunFactory>(Lifetime.Singleton);
            builder.Register<IWeaponFactory, LaserFactory>(Lifetime.Singleton);
        }
    }
}