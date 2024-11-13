using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(menuName = "Unit/" + nameof(AsteroidConfig), fileName = nameof(AsteroidConfig), order = 0)]
    internal sealed class AsteroidConfig : UnitConfig
    {
        [SerializeField, UnitId] private string[] _subAsteroids;
        [SerializeField, Min(0)] private int _subsCountMin;
        [SerializeField, Min(0)] private int _subsCountMax;
        
    }
}