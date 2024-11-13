using UnityEngine;

namespace Weapon
{
    public abstract class WeaponConfig : ScriptableObject
    {
        public abstract WeaponType Type { get; }
    }
}