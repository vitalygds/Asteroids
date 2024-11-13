namespace Weapon
{
    internal sealed class WeaponService : IWeaponService
    {
        private readonly WeaponUpdateManager _updateManager;
        private readonly IWeaponConfigLoader _configLoader;
        private readonly IWeaponFactory _factory;

        public WeaponService(WeaponUpdateManager updateManager, IWeaponConfigLoader configLoader, IWeaponFactory factory)
        {
            _updateManager = updateManager;
            _configLoader = configLoader;
            _factory = factory;
        }

        public void Clear()
        {
            _updateManager.Clear();
        }
        
        public IWeapon CreateWeapon(string id, WeaponCreationArgs args)
        {
            WeaponConfig config = _configLoader.Load(id);
            return _factory.CreateWeapon(config, args);
        }
    }
}