using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Unit
{
    [CreateAssetMenu(menuName = "Unit/" + nameof(ShipConfig), fileName = nameof(ShipConfig))]
    internal sealed class ShipConfig : UnitConfig
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _angularDrag;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _maxSpeed;
        
        [SerializeField, WeaponId] private string[] _weapons;
        public float RotationSpeed => _rotationSpeed;
        public float AngularSpeed => _angularSpeed;
        public float AngularDrag => _angularDrag;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float MaxSpeed => _maxSpeed;
        public IReadOnlyList<string> Weapons => _weapons;
        
        public override UnitType Type => UnitType.Ship;
    }
}