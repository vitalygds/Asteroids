namespace Unit
{
    public interface IUnitManager
    {
        uint GetNextId();
        void AddUnit(Unit unit);
        void DestroyUnit(uint id);
    }
}