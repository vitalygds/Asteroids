using System.Collections.Generic;

namespace Weapon
{
    internal sealed class WeaponCompositeFactory : IWeaponFactory
    {
        private readonly Dictionary<WeaponType, IWeaponFactory> _factoryMap;
        public WeaponType Type => WeaponType.None;

        public WeaponCompositeFactory(IReadOnlyList<IWeaponFactory> factories)
        {
            _factoryMap = new Dictionary<WeaponType, IWeaponFactory>(factories.Count);

            for (int i = 0; i < factories.Count; i++)
            {
                IWeaponFactory weaponFactory = factories[i];
                _factoryMap.Add(weaponFactory.Type, weaponFactory);
            }
        }
        
        public IWeapon CreateWeapon(WeaponConfig config, WeaponCreationArgs args)
        {
            if (_factoryMap.TryGetValue(config.Type, out IWeaponFactory factory))
            {
                return factory.CreateWeapon(config, args);
            }

            throw new KeyNotFoundException($"No factory registered for type {config.Type}");
        }
    }
}