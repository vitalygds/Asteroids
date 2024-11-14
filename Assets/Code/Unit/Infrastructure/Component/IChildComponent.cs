using System;

namespace Unit
{
    public interface IChildComponent : IUnitComponent
    {
        event Action<IUnit> OnChildCreated;
    }
}