using System;

namespace Unit
{
    public interface IUnit
    {
        event Action<IUnit> OnDestroy;
        uint Id { get; }
        bool TryGetComponent<T>(out T unitComponent) where T : class, IUnitComponent;
    }
}