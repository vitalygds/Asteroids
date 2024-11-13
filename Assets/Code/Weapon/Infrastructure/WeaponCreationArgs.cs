namespace Weapon
{
    public struct WeaponCreationArgs
    {
        public IWeaponUser User { get; }
        public IWeaponTargetProvider TargetProvider { get; }

        public WeaponCreationArgs(IWeaponUser user, IWeaponTargetProvider targetProvider)
        {
            User = user;
            TargetProvider = targetProvider;
        }
    }
}