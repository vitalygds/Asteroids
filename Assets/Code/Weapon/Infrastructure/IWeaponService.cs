namespace Weapon
{
    public interface IWeaponService
    {
        IWeapon CreateWeapon(string id, WeaponCreationArgs args);
    }
}