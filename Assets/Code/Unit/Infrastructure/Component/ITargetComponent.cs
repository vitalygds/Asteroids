using UnityEngine;

namespace Unit
{
    public interface ITargetComponent : IUnitComponent
    {
        void SetTarget(Transform target);
        void UnsetTarget();
    }
}