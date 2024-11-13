using Infrastructure;
using UnityEngine;

namespace Unit
{
    internal sealed class Player : Unit
    {
        private readonly IPoolService _poolService;
        private readonly Transform _view;

        public Player(uint id, IPoolService poolService, Transform view) : base(id)
        {
            _poolService = poolService;
            _view = view;
        }


        protected override void DestroyCompletely()
        {
            _poolService.Destroy(_view.gameObject);
        }
    }
}