using UnityEngine;

namespace Unit
{
    public interface IUnitSpawner
    {
        IUnit SpawnUnit(UnitCreationArgs args, Vector3 position, Quaternion rotation);
    }
}