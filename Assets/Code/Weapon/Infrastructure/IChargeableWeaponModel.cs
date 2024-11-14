namespace Weapon
{
    public interface IChargeableWeaponModel
    {
        int Charges { get; }
        int MaxCharges { get; }
        float Timer { get; }
    }
}