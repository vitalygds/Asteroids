using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(menuName = "Unit/" + nameof(AsteroidConfig), fileName = nameof(AsteroidConfig))]
    internal sealed class AsteroidConfig : UnitConfig
    {
        [SerializeField, UnitId] private string[] _subAsteroids;
        [SerializeField, Min(0)] private int _subsCountMin;
        [SerializeField, Min(0)] private int _subsCountMax;
        [SerializeField] private float _subsAdditiveForce;

        [SerializeField] private float _minSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _deceleration;
        public IReadOnlyList<string> SubAsteroids => _subAsteroids;
        public int SubsCountMin => _subsCountMin;
        public int SubsCountMax => _subsCountMax;
        public float SubsAdditiveForce => _subsAdditiveForce;
        public float MinSpeed => _minSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float Deceleration => _deceleration;

        public override UnitType Type => UnitType.Asteroid;
    }
}