using System.Collections.Generic;

namespace Weapon
{
    internal sealed class ChargeableModelMap : IChargeableModelMap
    {
        private readonly Dictionary<uint, IChargeableWeaponModel> _map = new();

        public bool TryGetChargeableWeapon(uint ownerId, out IChargeableWeaponModel weaponModel)
        {
            return _map.TryGetValue(ownerId, out weaponModel);
        }

        public void Add(uint ownerId, IChargeableWeaponModel model)
        {
            _map[ownerId] = model;
        }

        public void Remove(uint ownerId)
        {
            _map.Remove(ownerId);
        }
    }
}