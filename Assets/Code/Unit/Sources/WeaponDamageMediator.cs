using Weapon;

namespace Unit
{
    internal sealed class WeaponDamageMediator : IWeaponDamageMediator
    {
        private readonly IUnitIdService _idService;

        public WeaponDamageMediator(IUnitIdService idService)
        {
            _idService = idService;
        }

        public bool ApplyDamage(IDamageableView view, IWeaponUser from)
        {
            if (_idService.GetUnitById(view.Id, out IUnit unit) && unit.TryGetComponent(out IDestroyComponent destroyComponent))
            {
                destroyComponent.DestroyUnit();
                return true;
            }

            return false;
        }
    }
}