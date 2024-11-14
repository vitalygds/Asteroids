using UnityEngine;

namespace Unit
{
    public interface IMoveComponent : IUnitComponent
    {
        float Speed { get; }
        void SetPosition(Vector3 position);
        void SetDirection(Vector2 direction);
        void AddSpeed(float value);
    }
}