using System;

namespace Unit
{
    public interface IDestroyComponent : IUnitComponent
    {
        event Action OnDestroy;
        void DestroyUnit();
    }
}