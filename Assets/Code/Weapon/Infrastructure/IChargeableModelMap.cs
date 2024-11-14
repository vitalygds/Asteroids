namespace Weapon
{
    public interface IChargeableModelMap
    {
        bool TryGetChargeableWeapon(uint ownerId, out IChargeableWeaponModel weaponModel);
        void Add(uint ownerId, IChargeableWeaponModel model);
        void Remove(uint ownerId);
    }
}