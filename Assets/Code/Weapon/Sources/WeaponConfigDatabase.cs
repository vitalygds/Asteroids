using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "Database/" + nameof(WeaponConfigDatabase), fileName = nameof(WeaponConfigDatabase), order = 0)]
    internal sealed class WeaponConfigDatabase : ScriptableObject
    {
        [SerializeField] private WeaponConfig[] _configs;

        public IReadOnlyList<WeaponConfig> Configs => _configs;


#if UNITY_EDITOR
        internal void Editor_Initialize(WeaponConfig[] configs)
        {
            _configs = configs;
        }
#endif
    }
}