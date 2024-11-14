using System;
using UnityEngine;

namespace Unit
{
    public interface IUnit
    {
        event Action<IUnit> OnDestroy;
        uint Id { get; }
        Transform Transform { get; }
        bool TryGetComponent<T>(out T unitComponent) where T : class, IUnitComponent;
    }
}