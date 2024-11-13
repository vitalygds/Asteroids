using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(menuName = "Weapon/" + nameof(LaserConfig), fileName = nameof(LaserConfig))]
    public sealed class LaserConfig : WeaponConfig
    {
        [SerializeField, Header("Laser params")] private float _lenght;
        [SerializeField] private float _width;
        
        [SerializeField, Header("Charges")] private float _chargeTime;
        [SerializeField] private int _maxCharges;
        [SerializeField] private int _startCharges;
        
        [SerializeField, Header("Visualization")]
        private Color _color;
        [SerializeField] private float _visualTime;

        public float Lenght => _lenght;
        public float Width => _width;
        public float ChargeTime => _chargeTime;
        public int MaxCharges => _maxCharges;
        public int StartCharges => _startCharges;
        public Color Color => _color;
        public float VisualTime => _visualTime;
        public override WeaponType Type => WeaponType.Laser;
    }
}