using System.Collections.Generic;

namespace Unit
{
    internal sealed class UnitService : IUnitIdService, IUnitManager
    {
        private readonly Dictionary<uint, Unit> _unitMap = new();
        private uint _current;

        public uint GetNextId() => ++_current;

        public void AddUnit(Unit unit)
        {
            unit.OnDestroy += RemoveUnit;
            _unitMap.Add(unit.Id, unit);
        }
        
        public bool GetUnitById(uint id, out Unit unit) => _unitMap.TryGetValue(id, out unit);

        public bool GetUnitById(uint id, out IUnit unit)
        {
            if (_unitMap.TryGetValue(id, out Unit viewModel))
            {
                unit = viewModel;
                return true;
            }

            unit = null;
            return false;
        }

        private void RemoveUnit(IUnit unit)
        {
            unit.OnDestroy -= RemoveUnit;
            _unitMap.Remove(unit.Id);
        }
    }
}