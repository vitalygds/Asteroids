using System;
using UnityEngine;

namespace Weapon
{
    internal sealed class TriggerView : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggered;
        [SerializeField] private Collider2D _collider;

        public void SetInteractiveLayerMask(int mask)
        {
            LayerMask excludeLayers = -1;
            excludeLayers &= ~mask;
            _collider.excludeLayers = excludeLayers;
        }

        private void OnTriggerEnter2D(Collider2D other) => OnTriggered?.Invoke(other);
    }
}