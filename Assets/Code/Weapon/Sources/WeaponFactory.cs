using System;

namespace Weapon
{
    public abstract class WeaponFactory<TConfig> : IWeaponFactory where TConfig : WeaponConfig
    {
        public abstract WeaponType Type { get; }
        public IWeapon CreateWeapon(WeaponConfig config, WeaponCreationArgs args)
        {
            if (config is TConfig desired)
            {
                return CreateWeapon(desired, args);
            }

            throw new ArgumentException($"{config.name} is not {typeof(TConfig)}", nameof(config));
        }
        
        protected abstract IWeapon CreateWeapon(TConfig config, WeaponCreationArgs args);
    }
}