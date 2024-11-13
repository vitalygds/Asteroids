using Infrastructure;
using Input;
using UnityEngine;

namespace Unit
{
    public sealed class PlayerScope
    {
        public PlayerScope(IServiceLocator locator)
        {
            PlayerConstructor constructor = new PlayerConstructor(locator.Resolve<IUnitConfigLoader>(), locator.Resolve<IPoolService>(),
                locator.Resolve<ITickController>(), locator.Resolve<IInputService>(), locator.Resolve<IUnitManager>());
            constructor.CreatePlayer("DefaultPlayer", Vector3.zero, Quaternion.identity);
        }
    }
}