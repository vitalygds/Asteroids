namespace Weapon
{
    public interface IWeaponCompositeFactory
    {
        IWeapon CreateWeapon(WeaponConfig config, WeaponCreationArgs args);
    }
}