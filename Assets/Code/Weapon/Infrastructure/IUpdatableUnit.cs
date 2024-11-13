using System;

namespace Weapon
{
    public interface IUpdatableUnit
    {
        event Action<IUpdatableUnit> OnDestroy;
        void Update(float deltaTime);
        void Destroy();
    }
}