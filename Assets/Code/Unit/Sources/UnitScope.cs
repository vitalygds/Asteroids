using Infrastructure;
using Weapon;

namespace Unit
{
    public sealed class UnitScope
    {
        public static void Build(IServiceLocator locator)
        {
            locator.Register<IUnitConfigLoader>(new UnitConfigLoader());
            UnitService idService = new UnitService();
            locator.Register<IUnitManager>(idService);
            locator.Register<IUnitIdService>(idService);
            WeaponDamageMediator damageMediator = new WeaponDamageMediator(idService);
            locator.Register<IWeaponDamageMediator>(damageMediator);
        }

        public static void Complete(IServiceLocator locator)
        {
            PlayerScope.Build(locator);
        }
    }
}