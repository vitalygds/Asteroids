using Infrastructure;
using UnityEngine.InputSystem.UI;

namespace Input
{
    public sealed class InputScope
    {
        public InputScope(IServiceLocator locator)
        {
            InputSystemUIInputModule inputModule = locator.Resolve<InputSystemUIInputModule>();
            ITickController tickController = locator.Resolve<ITickController>();
            InputService inputService = new InputService(inputModule, tickController);
            locator.Register<IInputService>(inputService);
        }
    }
}