using UnityEngine;
using Weapon;

namespace Unit
{
    [CreateAssetMenu(menuName = "Unit/" + nameof(UfoConfig), fileName = nameof(UfoConfig))]
    internal sealed class UfoConfig : UnitConfig
    {
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _maxSpeed;
        [SerializeField, WeaponId] private string _weapon;
        public float AngularSpeed => _angularSpeed;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float MaxSpeed => _maxSpeed;
        public string Weapon => _weapon;
        public override UnitType Type => UnitType.Ufo;
    }
}