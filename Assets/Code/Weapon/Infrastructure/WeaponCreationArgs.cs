namespace Weapon
{
    public struct WeaponCreationArgs
    {
        public IWeaponUser User { get; }
        public IWeaponTargetProvider TargetProvider { get; }
        
        public int TargetLayerMask { get; }

        public WeaponCreationArgs(IWeaponUser user, IWeaponTargetProvider targetProvider, int targetLayerMask)
        {
            User = user;
            TargetProvider = targetProvider;
            TargetLayerMask = targetLayerMask;
        }
    }
}