using System.Collections.Generic;

namespace Weapon
{
    public static class WeaponCollections
    {
        public static Dictionary<TK, TV> GetMap<TK, TV>()
        {
#if COLLECTIONPOOLING_WEAPON
            return UnityEngine.Pool.DictionaryPool<TK, TV>.Get();
#else
            return new Dictionary<TK, TV>();
#endif
        }

        public static List<T> GetList<T>()
        {
#if COLLECTIONPOOLING_WEAPON
            return UnityEngine.Pool.ListPool<T>.Get();
#else
            return new List<T>();
#endif
        }

        public static HashSet<T> GetHashSet<T>()
        {
#if COLLECTIONPOOLING_WEAPON
            return UnityEngine.Pool.HashSetPool<T>.Get();
#else
            return new HashSet<T>();
#endif
        }

        public static void Release<TK, TV>(Dictionary<TK, TV> map)
        {
#if COLLECTIONPOOLING_WEAPON
            UnityEngine.Pool.DictionaryPool<TK, TV>.Release(dict);
#else
            map.Clear();
#endif
        }

        public static void Release<T>(List<T> list)
        {
#if COLLECTIONPOOLING_WEAPON
            UnityEngine.Pool.ListPool<T>.Release(list);
#else
            list.Clear();
#endif
        }


        public static void Release<T>(HashSet<T> set)
        {
#if COLLECTIONPOOLING_WEAPON
            UnityEngine.Pool.HashSetPool<T>.Release(list);
#else
            set.Clear();
#endif
        }
    }
}