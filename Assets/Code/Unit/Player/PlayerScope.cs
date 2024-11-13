using Infrastructure;
using Input;
using UnityEngine;
using Weapon;

namespace Unit
{
    public sealed class PlayerScope
    {
        public static void Build(IServiceLocator locator)
        {
            PlayerConstructor constructor = new PlayerConstructor(locator.Resolve<IUnitConfigLoader>(), locator.Resolve<IPoolService>(),
                locator.Resolve<ITickController>(), locator.Resolve<IInputService>(), locator.Resolve<IUnitManager>(),
                locator.Resolve<IWeaponService>());
            constructor.CreatePlayer("DefaultPlayer", Vector3.zero, Quaternion.identity);
        }
    }
}