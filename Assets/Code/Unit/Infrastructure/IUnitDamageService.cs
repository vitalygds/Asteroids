using System;

namespace Unit
{
    public interface IUnitDamageService
    {
        event Action<uint, IUnit> OnUnitDamaged;
        bool ApplyDamage(uint to, IUnit from);
    }
}