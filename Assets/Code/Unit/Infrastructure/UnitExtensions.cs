using System;
using System.Collections.Generic;

namespace Unit
{
    public static class UnitExtensions
    {
        public static T GetComponent<T>(this IUnit unit) where T : class, IUnitComponent
        {
            if (unit.TryGetComponent(out T component))
            {
                return component;
            }

            throw new ArgumentException($"Unit {unit} does not have a component {typeof(T)}");
        }

        public static bool TryGetCachedComponent<T, TU>(this Dictionary<Type, TU> map, IList<TU> collection, out T component) where T : TU
        {
            Type type = typeof(T);
            if (map.TryGetValue(type, out TU unitComponent))
            {
                component = (T) unitComponent;
                return true;
            }

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i] is T target)
                {
                    map[type] = target;
                    component = target;
                    return true;
                }
            }

            component = default;
            return false;
        }
    }
}