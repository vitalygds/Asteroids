using UnityEngine;

namespace Unit
{
    public interface IUnitConstructor
    {
        public UnitType Type { get; }
        IUnit CreateUnit(UnitCreationArgs args, UnitConfig config, Vector3 position, Quaternion rotation);
    }
}