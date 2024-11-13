namespace Weapon
{
    public interface IWeaponDamageMediator
    {
        bool ApplyDamage(IDamageableView view, IWeaponUser from);
    }
}