using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class UnitObstacleComponent : IUnitComponent, IDestroyable
    {
        private readonly Unit _owner;
        private readonly UnitView _view;
        private readonly IUnitDamageService _damageService;
        private readonly int _layerMask;

        public UnitObstacleComponent(Unit owner, UnitView view, IUnitDamageService damageService, int layerMask)
        {
            _owner = owner;
            _view = view;
            _damageService = damageService;
            _layerMask = layerMask;
            view.OnTriggered += CheckTarget;
        }

        public void Destroy()
        {
            _view.OnTriggered -= CheckTarget;
        }

        private void CheckTarget(Collider2D other)
        {
            if (other.gameObject.IsLayerOfMask(_layerMask))
            {
                if (other.gameObject.TryGetComponent(out UnitView view))
                {
                    _damageService.ApplyDamage(view.Id, _owner);
                }
            }
        }
    }
}