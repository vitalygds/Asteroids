using System;

namespace Unit
{
    internal sealed class DestroyComponent : IDestroyComponent
    {
        public event Action OnDestroy;
        private readonly Unit _unit;

        public DestroyComponent(Unit unit)
        {
            _unit = unit;
        }

        public void DestroyUnit()
        {
            OnDestroy?.Invoke();
            _unit.Destroy();
        }
    }
}