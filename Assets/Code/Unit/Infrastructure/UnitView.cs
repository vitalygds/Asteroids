using System;
using UnityEngine;
using Weapon;

namespace Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitView : MonoBehaviour, IDamageableView
    {
        [SerializeField] private GameObject[] _childToSetLayer;
        public event Action<Collider2D> OnTriggered;
        public uint Id { get; private set; }

        public void Initialize(uint id, int layer)
        {
            Id = id;
            for (int i = 0; i < _childToSetLayer.Length; i++)
            {
                _childToSetLayer[i].layer = layer;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) => OnTriggered?.Invoke(other);
    }
}