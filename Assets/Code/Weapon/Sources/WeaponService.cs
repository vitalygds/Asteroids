namespace Weapon
{
    internal sealed class WeaponService : IWeaponService
    {
        private readonly IWeaponConfigLoader _configLoader;
        private readonly IWeaponFactory _factory;

        public WeaponService(IWeaponConfigLoader configLoader, IWeaponFactory factory)
        {
            _configLoader = configLoader;
            _factory = factory;
        }
        
        public IWeapon CreateWeapon(string id, WeaponCreationArgs args)
        {
            WeaponConfig config = _configLoader.Load(id);
            return _factory.CreateWeapon(config, args);
        }
    }
}