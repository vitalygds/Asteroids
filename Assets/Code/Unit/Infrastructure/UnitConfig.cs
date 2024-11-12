using UnityEngine;

namespace Unit
{
    public abstract class UnitConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
    }
}