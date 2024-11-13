using UnityEngine;

namespace Weapon
{
    public interface IWeaponTargetProvider
    {
        Vector3 StartPosition { get; }
        Vector3 EndPosition { get; }
    }
}