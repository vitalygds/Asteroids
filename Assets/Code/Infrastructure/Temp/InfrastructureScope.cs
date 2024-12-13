using VContainer;

namespace Infrastructure
{
    public sealed class InfrastructureScope
    {
        public static void Build(IContainerBuilder builder)
        {
            builder.Register<ITickController, TickController>(Lifetime.Singleton);
            builder.Register<ITimeManager, TimeManager>(Lifetime.Singleton);
            builder.Register<IPoolService, PoolService>(Lifetime.Singleton);
            builder.Register<IRandomizer, Randomizer>(Lifetime.Singleton);
        }
    }
}