using UnityEngine;

namespace Unit
{
    public abstract class UnitConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _moveSpeed;
    }
}