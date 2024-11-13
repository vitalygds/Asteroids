namespace Weapon
{
    public interface IWeaponFactory
    {
        WeaponType Type { get; }
        IWeapon CreateWeapon(WeaponConfig config, WeaponCreationArgs args);
    }
}