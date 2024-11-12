using Infrastructure;
using Input;

namespace Unit
{
    public sealed class PlayerScope
    {
        public PlayerScope(IServiceLocator locator)
        {
            PlayerConstructor constructor = new PlayerConstructor(locator.Resolve<IUnitConfigLoader>(), locator.Resolve<IPoolService>(),
                locator.Resolve<ITickController>(), locator.Resolve<IInputService>(), locator.Resolve<IUnitManager>());
            constructor.CreatePlayer("DefaultPlayer");
        }
    }
}