using VContainer;

namespace Input
{
    public sealed class InputScope
    {
        public static void Build(IContainerBuilder builder)
        {
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
        }
    }
}