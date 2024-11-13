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
            return true;
        }
    }
}