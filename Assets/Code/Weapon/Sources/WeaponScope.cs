using Infrastructure;

namespace Weapon
{
    public sealed class WeaponScope
    {
        public WeaponScope(IServiceLocator locator)
        {
            WeaponConfigLoader loader = new WeaponConfigLoader();
            locator.Register<IWeaponConfigLoader>(loader);
        }
    }
}