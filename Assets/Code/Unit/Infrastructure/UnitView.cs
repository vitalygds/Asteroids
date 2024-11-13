using UnityEngine;
using Weapon;

namespace Unit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitView : MonoBehaviour, IDamageableView
    {
        [SerializeField] private GameObject[] _childToSetLayer;
        public uint Id { get; private set; }

        public void Initialize(uint id, int layer)
        {
            Id = id;
            for (int i = 0; i < _childToSetLayer.Length; i++)
            {
                _childToSetLayer[i].layer = layer;
            }
        }
    }
}