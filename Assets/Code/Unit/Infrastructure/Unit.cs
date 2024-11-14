using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public abstract class Unit : IUnit, IEquatable<Unit>, IEquatable<IUnit>
    {
        public event Action<IUnit> OnDestroy;
        private readonly List<IUnitComponent> _components;
        private readonly Dictionary<Type, IUnitComponent> _componentsMap;
        private bool _destroyed;

        public uint Id { get; }
        public string Name { get; }
        public Transform Transform { get; }

        protected Unit(string name, uint id, Transform transform)
        {
            Id = id;
            Transform = transform;
            Name = name;
            _components = UnitCollections.GetList<IUnitComponent>();
            _componentsMap = UnitCollections.GetMap<Type, IUnitComponent>();
        }
        
        public void Destroy()
        {
            if (_destroyed)
                return;
            _destroyed = true;
            OnDestroy?.Invoke(this);
            DestroyComponents();
            UnitCollections.Release(_components);
            UnitCollections.Release(_componentsMap);
            DestroyCompletely();
        }

        public void AddComponent(IUnitComponent component)
        {
            _components.Add(component);
        }
        
        public bool TryGetComponent<T>(out T component) where T : class, IUnitComponent
        {
            return _componentsMap.TryGetCachedComponent(_components, out component);
        }

        protected abstract void DestroyCompletely();

        private void DestroyComponents()
        {
            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] is IDestroyable destroyableComponent)
                    destroyableComponent.Destroy();
            }

            _components.Clear();
        }

        public bool Equals(IUnit other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        public bool Equals(Unit other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Unit) obj);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public static bool operator ==(Unit left, Unit right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Unit left, Unit right)
        {
            return !Equals(left, right);
        }
    }
}