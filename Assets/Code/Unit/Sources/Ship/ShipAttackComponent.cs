using System.Collections.Generic;
using Infrastructure;
using UnityEngine;
using Weapon;

namespace Unit
{
    internal sealed class ShipAttackComponent : IAttackComponent, IDestroyable, IWeaponUser, IWeaponTargetProvider
    {
        private readonly Unit _owner;
        private readonly Transform _view;
        private readonly List<IWeapon> _weapons;
        private int _bits;
        public uint Id => _owner.Id;
        public Vector3 StartPosition => _view.position;
        public Vector3 EndPosition => _view.position + _view.up;

        public ShipAttackComponent(Unit owner, Transform view)
        {
            _owner = owner;
            _view = view;
            _weapons = UnitCollections.GetList<IWeapon>();
        }

        public void AddWeapon(IWeapon weapon)
        {
            _weapons.Add(weapon);
        }

        public void SetActive(int value, bool isActive)
        {
            if (value < 0 || value >= _weapons.Count)
                return;
            int bitsByDre = 1 << value;
            if (isActive == IsSet(bitsByDre))
                return;

            if (isActive)
            {
                _bits |= bitsByDre;
                _weapons[value].SetActive(true);
            }
            else
            {
                _bits &= ~bitsByDre;
                _weapons[value].SetActive(false);
            }
        }
        
        private bool IsSet(int valueBit) => (_bits & valueBit) != 0;


        public void Destroy()
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                _weapons[i].Destroy();
            }

            UnitCollections.Release(_weapons);
        }
    }
}