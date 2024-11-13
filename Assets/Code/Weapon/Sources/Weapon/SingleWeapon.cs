using System;

namespace Weapon
{
    internal sealed class SingleWeapon : IWeapon, IUpdatableUnit
    {
        public event Action<IUpdatableUnit> OnDestroy;
        private readonly IWeaponModel _model;
        private readonly WeaponConfig _config;
        private float _time;
        private bool _charged;
        private bool _isActive;
        
        public SingleWeapon(IWeaponModel model, WeaponConfig config)
        {
            _model = model;
            _config = config;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }
        
        private void Attack()
        {
            _charged = false;
           _model.Attack();
            _time = _config.ReloadTime;
        }

        void IUpdatableUnit.Update(float deltaTime)
        {
            _time -= deltaTime;
            if (_time <= 0)
            {
                _time = float.PositiveInfinity;
                _charged = true;
            }

            if (_charged && _isActive)
            {
                Attack();
            }
        }
    }
}