using System;
using Weapon;

namespace Unit
{
    internal sealed class UnitDamageService : IWeaponDamageMediator, IUnitDamageService
    {
        public event Action<uint, IUnit> OnUnitDamaged;
        private readonly IUnitIdService _idService;

        public UnitDamageService(IUnitIdService idService)
        {
            _idService = idService;
        }

        public bool ApplyDamage(uint to, IUnit from)
        {
            return ApplyDamage(to, from.Id);
        }

        public bool ApplyDamage(IDamageableView view, IWeaponUser from)
        {
            return ApplyDamage(view.Id, from.Id);
        }

        private bool ApplyDamage(uint to, uint fromId)
        {
            if (_idService.GetUnitById(to, out IUnit unit) && unit.TryGetComponent(out IDestroyComponent destroyComponent))
            {
                OnUnitDamaged?.Invoke(fromId, unit);
                destroyComponent.DestroyUnit();
                return true;
            }

            return false;
        }
    }
}