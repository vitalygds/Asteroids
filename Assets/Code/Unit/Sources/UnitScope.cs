using Infrastructure;

namespace Unit
{
    public sealed class UnitScope
    {
        public UnitScope(IServiceLocator locator)
        {
            locator.Register<IUnitConfigLoader>(new UnitConfigLoader());
            UnitService idService = new UnitService();
            locator.Register<IUnitManager>(idService);
            locator.Register<IUnitIdService>(idService);
            
            new PlayerScope(locator);
        }
    }
}