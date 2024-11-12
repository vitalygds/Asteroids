namespace Infrastructure
{
    public sealed class InfrastructureScope
    {
        public InfrastructureScope(IServiceLocator locator)
        {
            TimeManager timeManager = new TimeManager();
            locator.Register<ITimeManager>(timeManager);
            TickController tickController = new TickController(timeManager);
            locator.Register<ITickController>(tickController);
            locator.Register<IPoolService>(new PoolService(tickController));
        }
    }
}