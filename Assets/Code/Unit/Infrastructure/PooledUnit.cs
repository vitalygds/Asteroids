using Infrastructure;

namespace Unit
{
    public class PooledUnit : Unit
    {
        private readonly IPoolService _poolService;
        private readonly UnitView _view;

        public PooledUnit(string name, uint id, IPoolService poolService, UnitView view) : base(name, id, view.transform)
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