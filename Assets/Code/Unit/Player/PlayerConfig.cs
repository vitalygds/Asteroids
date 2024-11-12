using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(menuName = "Unit/" + nameof(PlayerConfig), fileName = nameof(PlayerConfig), order = 0)]
    internal sealed class PlayerConfig : UnitConfig
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _maxSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float AngularSpeed => _angularSpeed;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float MaxSpeed => _maxSpeed;
    }
}