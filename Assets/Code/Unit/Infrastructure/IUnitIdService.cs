namespace Unit
{
    public interface IUnitIdService
    {
        bool GetUnitById(uint id, out IUnit unit);
    }
}